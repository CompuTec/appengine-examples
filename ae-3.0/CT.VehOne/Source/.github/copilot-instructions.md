# AppEngine Multi‑Plugin Template – AI Assistant Rules

Purpose: Enable rapid, correct changes in a template solution built on CompuTec AppEngine (Core2) for SAP Business One. Keep guidance generic (no domain assumptions) so teams can specialize later.
 
## 1. Solution Structure (Typical Folders)
- `*.BL`  (Business Logic Library)
  - Database / setup (`DatabaseSetup/**`, scripts load order XML, custom fields, authorizations)
  - Business entities (UDO interfaces & enumerations)
  - IoC services (connection‑scoped logic)
  - Translations (`Translations/*.xml`)
  - Manifest + setup & plugin info classes
- `*` (Web Plugin)
  - OData controllers (partial; generated base + custom partials)
  - Minimal API endpoint builders
  - Jobs (event bus, recurring, background)
  - Custom views (`CustomViews/*.customview.json`)
  - Generated artifacts (`CtGeneratedCode/**`) – models / serializers / controller bases (DO NOT EDIT)
  - Frontend resources (`www/webapp/**`) copied as content
- `*.UI` (SAP UI Plugin)
  - Menus / forms (`UI/**.xml`), translation files
  - Manifest with SAPUI section
  - Objects (UDO UI handlers extending `BaseUIBeanLoader<T>`)
- Optional additional plugin(s) (e.g. integrations or sample jobs)
- `Global/*.Props` centralize package versions (use these instead of per‑project duplicates)

## 2. Build & Code Generation
- Target framework: `net8.0` across all projects.
- Run `dotnet build` in `Source/` root; MSBuild targets execute packaging (`CTpack`) automatically (avoid duplicating those targets).
- BL project AfterBuild runs `dotnet ct gen` to emit OData models / serializers / controller base classes into the web plugin (`CtGeneratedCode/`). Extend behavior by modifying BL source (entities/services) then rebuild—never edit generated files directly.
- Frontend (if used) under `www/`; MSBuild excludes `src`, `dist`, `node_modules` from compilation and copies `webapp` at build.
- Always run `dotnet tool restore` once after cloning to ensure `ct` CLI availability.

## 3. Configuration & Manifests
- Each plugin has a `manifest.json` (id, PluginType: `Di`, `plugin`, or `SapUI`; dependencies; translations; UI tiles/forms).
- `dev.config.json` defines: output DLL path, dependency chain (relative paths to other `dev.config.json` files) and whether to map `www`.
- Plugin runtime configuration: register via `SetConfiguration<TConfig>()` in `Info.cs`. Extend by adding properties to the config class; resolve with DI (`GetService<TConfig>()`).
- After cloning, update any absolute paths in `Properties/*.config.json` / launch settings – do not hardcode environment‑specific paths in repository edits.

## 4. Dependency Management
- Shared NuGet versions controlled by `Global/*.Props` (e.g., Core2, AE, UI). Add new shared packages there; project‑specific packages only when truly isolated.
- Reference other solution projects with `<ProjectReference>`; rely on manifests + `dev.config.json` for runtime dependency order.

## 5. IoC & Services Pattern
- Annotate services with `[Ioc(Scope = IocScope.Connection, ReferenceType = typeof(IMyInterface))]` (connection scope typical for DI / UDO access).
- Acquire services / UDO interfaces only through an `ICoreConnection` (`_coreConnection.GetService<T>()` or inside jobs `GetService<T>()`). Never `new` them.
- Prefer structured logging with placeholders and optional timing scopes (`_logger.CreateMeasure()`).

## 6. Jobs
- Base types: `SecureJob` (runs with secure connection) and `EventBusSecureJob` (triggered by entity events).
- Attributes:
  - `[EventBusJob(JobId = ..., Description = ..., ActionType = "A|U|*", ContentType = "<object code>")]`
  - `[RecurringJob(JobId = ..., JobName = ..., CronExpression = "0 0 0 ? * * *")]` (Quartz 7‑field syntax)
  - `[BackgroundJob(JobId = ..., JobName = ..., EmitsProgressBar = true)]`
- Pattern: public constants for JobId / JobName; constructor `(IAeJobInvoker, ILogger<>)`; override `Call()` (async supported). Use `BeforeCall(QueryManager qm)` in event bus jobs for cheap guard filtering before heavier logic.

## 7. OData & Minimal APIs
- Extend generated controller partials (e.g., add actions) without modifying generated base.
- Action flow: validate input → fetch entity via helper (e.g., `GetEntityByKey`) → manipulate child collections (set last line, add new if filled) → call `Update()` / `Add()` → serialize using existing serializer.
- Minimal APIs: create subclass of `AeSecureMinimalApiEndpointBuilder`, override `Route`, map endpoints in `Configure(...)`, inject services using `SecureScopeService<T>` where secure context is required (e.g., translation service).

## 8. Database Setup & Versioning
- `SetupInfo` derivative sets `CurrentDBVersion`, script load order XML, and toggles embedded SQL resources.
- Increment DB version + manifest versions when structural or authorization changes occur.
- Add custom fields via `GetUserDefinedFields` (respect SAP limits for sizes / types).
- Authorizations: define hierarchical permissions in a dedicated class deriving from authorization base (constants + list of `DocumentAuthorizationInfo`).

## 9. Custom Views
- Each `*.customview.json` has dual SQL sources: `{ "Hana": "...", "MsSql": "..." }`. Always provide both (keep field casing consistent with target DB). Store under the plugin's `CustomViews/` folder.

## 10. Translations
- Translation XML referenced in plugin manifests (`TranslationDescription.XmlTranslationFile`, UI `translationFile`).
- At runtime access messages via translation service (`ITranslationService`) rather than hardcoding text.
- Maintain stable keys; add new keys systematically across relevant language files.

## 11. SAP UI API (Classic Client) Development
**Purpose**: Handle UDO form interactions in SAP B1 client using `BaseUIBeanLoader<T>` pattern.

**Structure:**
- `Objects/**/*UI.cs` – Form handlers extending `BaseUIBeanLoader<IEntity>`
- `UI/Forms/**/*Form.cs` – Form definition constants (FormTypeEx, Controls)
- `Services/**/*UiTools.cs` – Helper services for UI operations

**BaseUIBeanLoader Pattern:**
```csharp
public partial class MyEntityUI : BaseUIBeanLoader<IMyEntity>
{
    // Constructor: (ICoreConnection, ILogger<>, ITranslationService)
    
    protected override UserInterfaceUdoDefinitions GetDefinitions()
    {
        // Define form structure: tabs, matrices, user data sources
        var definitions = getGeneratedDefinitions();
        definitions.ForForm(MyEntityForm.FormTypeEx).SetTabs(
            TabDef(MyEntityForm.Controls.Tab1, 1),
            TabDef(MyEntityForm.Controls.Tab2, 2, MyEntityForm.Controls.Matrix)
        );
        return definitions;
    }
    
    protected override void FillAllUserDataSource(IMyEntity bean, Form frm)
    {
        // Populate calculated/lookup fields when form loads/navigates
        var tool = _CoreConnection.GetService<IMyEntityUiTools>();
        bean.SetCalculatedField(tool.GetLookupValue(bean.SomeProperty));
    }
}
```

**User Data Sources (UDS):**
- Define with `[GenerateUiUdoDefinition]` and nested classes
- Use `[UDS(UdsType = typeof(T), UdsUniqueId = "...")]` for form field mappings
- Generated extension methods: `bean.SetFieldName(value)` for UDS population
- Support header UDS and matrix-specific UDS (specify `Matrix = "controlId"`)

**Events & LoadFromTo Pattern:**
- **LoadFromTo Events**: Override `LoadFromTo()` for inter-form navigation and data transfer
- **Form Events**: Handle via `OnFormItemEvent()`, `OnFormDataEvent()`, `OnFormMenuEvent()`
- **Field Events**: Use `ItemEvent` handlers for validation, lookups, calculations
- **Matrix Events**: Handle row selection, cell changes, add/delete operations

**Event Handler Patterns:**
```csharp
protected override bool OnFormItemEvent(ref ItemEvent pVal)
{
    // Handle field events: validation, lookups, Choose From List
    if (pVal.ItemUID == MyForm.Controls.LookupField && pVal.EventType == BoEventTypes.et_LOST_FOCUS)
    {
        // Validate or populate related fields
        return HandleFieldChange(pVal);
    }
    return base.OnFormItemEvent(ref pVal);
}
 private void HandleFieldChange(ItemEvent pVal)
 {
  LoadFromTo(LoadFromToAction(itemEvent.CurrentForm, udo => { /*your logic on what to do with udo goes here */ }));
 }
```

**Integration:**
- Inject IoC services via constructor for business logic separation
- Access via `_CoreConnection.GetService<IMyUiTools>()`
- Use translation service for localized messages
- Form XML definitions in `UI/` folder reference FormTypeEx

**Conventions:**
DO:
- Keep UI logic in `BaseUIBeanLoader` descendants
- Use UDS for calculated/lookup fields vs. direct form manipulation
- Override specific event methods (`OnFormItemEvent`, `LoadFromTo`) for custom behavior
- Separate data access into dedicated UI service classes
- Define form constants in separate `*Form.cs` classes

AVOID:
- Direct SAP UI API calls (use base class methods)
- Business logic in UI classes (delegate to services)
- Hardcoded form field references (use generated constants)
- Complex logic in event handlers (extract to service methods)

## 12. Frontend Development (UI5 TypeScript)
Located in `www/webapp/` (copied to build output):

**Structure:**
- `controller/` – View controllers extending `BaseController` or `sap/ui/core/mvc/Controller`
- `view/` – XML views (`.view.xml`) with data binding syntax
- `model/` – Data models, formatters, validators
- `service/` – Business service classes for OData/API calls
- `manifest.json` – App descriptor (routing, models, dependencies)

**Conventions:**
- **Controllers**: Name matches view (`List.controller.ts` ↔ `List.view.xml`). Use dependency injection pattern: `this.getModel("odata")` for backend data, `this.getModel()` for view/JSON models.
- **Views**: Fragment reuse via `<core:Fragment fragmentName="..." type="XML" />`. Bind to OData entities: `{path: '/EntitySet', parameters: {...}}` or JSON models: `{/localProperty}`.
- **Services**: Create separate classes in `service/` for complex business logic. Return promises for async operations. Handle errors consistently (MessageToast, MessageBox).
- **Routing**: Define in `manifest.json` "routing" section. Navigate via `this.getRouter().navTo("routeName", {param: value})`.
- **Models**: Register in `manifest.json` "models" section. OData V4 model for backend, JSONModel for local state/calculations.

**Patterns:**
- **Master-Detail**: Use `sap.m.SplitApp` or flexible column layout with parameter passing via routing
- **CRUD Operations**: Controller methods call service layer → service makes OData calls → refresh model/view on success
- **Validation**: Extend `sap.ui.core.mvc.Controller`, implement `onValidateInput()` patterns, use model type constraints
- **Error Handling**: Centralized in base controller or service layer, consistent user messaging

DO:
- Keep controllers thin (delegate to services)
- Use TypeScript interfaces for type safety
- Leverage UI5 data binding vs. manual DOM manipulation
- Follow SAP Fiori design guidelines for UX consistency

AVOID:
- Direct OData calls in controllers (use service layer)
- Mixing business logic in view controllers
- Hardcoded text (use i18n model)

## 13. Conventions & DO / AVOID
DO:
- Centralize package refs in `Global/*.Props`.
- Use QueryManager helper methods (`ExecuteSimpleQuery<T>`, `ValueExists`) for simple lookups instead of raw SQL strings.
- Keep custom logic in partials / separate classes; let generation own its directories.
- Guard event bus jobs early (string / flag checks) to reduce load.

AVOID:
- Editing anything inside `CtGeneratedCode/**`.
- Duplicating packaging or generation MSBuild targets.
- Hardcoding environment paths in committed files.
- Modifying manifest dependency versions without aligning shared `CTCoreVersion`.

## 14. Extending the Template (Typical Flow)
1. Add / modify business entity or service in BL (with IoC attribute) → build to regenerate web artifacts.
2. Introduce job (choose attribute type) → implement guard / logic using injected services.
3. Expose new capability via OData (update model builder in `Info` + controller partial) or Minimal API endpoint builder.
4. If access control needed: add permission constants + authorization entries.
5. SAP UI: Create `*UI.cs` handler with UDS definitions → implement `GetDefinitions()`, `FillAllUserDataSource()`, event handlers → add form XML.
6. Frontend: Add new view/controller pair → register route in manifest → create service class for data operations.
7. Add/adjust translations & bump versions where user‑visible changes occur.

Keep instructions concise. Expand only with patterns proven in code (not aspirational). Provide further detail on request (e.g., UI form lifecycle, advanced query patterns, deployment bundling).(pVal);
}
return base.OnFormItemEvent(ref pVal);
}


```

**Integration:**
- Inject IoC services via constructor for business logic separation
- Access via `_CoreConnection.GetService<IMyUiTools>()`
- Use translation service for localized messages
- Form XML definitions in `UI/` folder reference FormTypeEx

**Conventions:**
DO:
- Keep UI logic in `BaseUIBeanLoader` descendants
- Use UDS for calculated/lookup fields vs. direct form manipulation
- Override specific event methods (`OnFormItemEvent`, `LoadFromTo`) for custom behavior
- Separate data access into dedicated UI service classes
- Define form constants in separate `*Form.cs` classes

AVOID:
- Direct SAP UI API calls (use base class methods)
- Business logic in UI classes (delegate to services)
- Hardcoded form field references (use generated constants)
- Complex logic in event handlers (extract to service methods)

## 12. Frontend Development (UI5 TypeScript)
Located in `www/webapp/` (copied to build output):

**Structure:**
- `controller/` – View controllers extending `BaseController` or `sap/ui/core/mvc/Controller`
- `view/` – XML views (`.view.xml`) with data binding syntax
- `model/` – Data models, formatters, validators
- `service/` – Business service classes for OData/API calls
- `manifest.json` – App descriptor (routing, models, dependencies)

**Conventions:**
- **Controllers**: Name matches view (`List.controller.ts` ↔ `List.view.xml`). Use dependency injection pattern: `this.getModel("odata")` for backend data, `this.getModel()` for view/JSON models.
- **Views**: Fragment reuse via `<core:Fragment fragmentName="..." type="XML" />`. Bind to OData entities: `{path: '/EntitySet', parameters: {...}}` or JSON models: `{/localProperty}`.
- **Services**: Create separate classes in `service/` for complex business logic. Return promises for async operations. Handle errors consistently (MessageToast, MessageBox).
- **Routing**: Define in `manifest.json` "routing" section. Navigate via `this.getRouter().navTo("routeName", {param: value})`.
- **Models**: Register in `manifest.json` "models" section. OData V4 model for backend, JSONModel for local state/calculations.

**Patterns:**
- **Master-Detail**: Use `sap.m.SplitApp` or flexible column layout with parameter passing via routing
- **CRUD Operations**: Controller methods call service layer → service makes OData calls → refresh model/view on success
- **Validation**: Extend `sap.ui.core.mvc.Controller`, implement `onValidateInput()` patterns, use model type constraints
- **Error Handling**: Centralized in base controller or service layer, consistent user messaging

DO:
- Keep controllers thin (delegate to services)
- Use TypeScript interfaces for type safety
- Leverage UI5 data binding vs. manual DOM manipulation
- Follow SAP Fiori design guidelines for UX consistency

AVOID:
- Direct OData calls in controllers (use service layer)
- Mixing business logic in view controllers
- Hardcoded text (use i18n model)

## 13. Conventions & DO / AVOID
DO:
- Centralize package refs in `Global/*.Props`.
- Use QueryManager helper methods (`ExecuteSimpleQuery<T>`, `ValueExists`) for simple lookups instead of raw SQL strings.
- Keep custom logic in partials / separate classes; let generation own its directories.
- Guard event bus jobs early (string / flag checks) to reduce load.

AVOID:
- Editing anything inside `CtGeneratedCode/**`.
- Duplicating packaging or generation MSBuild targets.
- Hardcoding environment paths in committed files.
- Modifying manifest dependency versions without aligning shared `CTCoreVersion`.

## 14. Extending the Template (Typical Flow)
1. Add / modify business entity or service in BL (with IoC attribute) → build to regenerate web artifacts.
2. Introduce job (choose attribute type) → implement guard / logic using injected services.
3. Expose new capability via OData (update model builder in `Info` + controller partial) or Minimal API endpoint builder.
4. If access control needed: add permission constants + authorization entries.
5. SAP UI: Create `*UI.cs` handler with UDS definitions → implement `GetDefinitions()`, `FillAllUserDataSource()`, event handlers → add form XML.
6. Frontend: Add new view/controller pair → register route in manifest → create service class for data operations.
7. Add/adjust translations & bump versions where user‑visible changes occur.

Keep instructions concise. Expand only with patterns proven in code (not aspirational). Provide further detail on request (e.g., UI form lifecycle, advanced query patterns, deployment bundling).