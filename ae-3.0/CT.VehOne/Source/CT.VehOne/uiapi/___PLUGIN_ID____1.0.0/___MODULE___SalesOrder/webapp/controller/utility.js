define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.Assert = void 0;
    exports.StringJoin = StringJoin;
    class Assert {
        static equal(actual, expected, message) {
            console.assert(actual == expected, message);
            if (actual != expected) {
                const details = `actual is '${actual}', expected should be '${expected}'`;
                throw new Error(message + "\n" + details);
            }
        }
        static notEqual(actual, expected, message) {
            console.assert(actual != expected, message);
            if (actual == expected) {
                const details = `actual is '${actual}', expected should not be '${expected}'`;
                throw new Error(message + "\n" + details);
            }
        }
    }
    exports.Assert = Assert;
    function StringJoin(arr, sep) {
        return arr.join(sep);
    }
});

//# sourceMappingURL=utility.js.map
