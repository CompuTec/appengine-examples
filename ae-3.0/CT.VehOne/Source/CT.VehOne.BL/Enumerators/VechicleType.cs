using CompuTec.Core2.DI.Attributes;

namespace CT.VehOne.BL.Enumerators;

[EnumType(new int[] { 1, 2,3,4,5 }, new string[] { "C", "B" ,"T","M","O"}, 1)]
public enum VechicleType
{
    Car = 1,
    Bus = 2,
    Truck = 3,
    Motorcycle = 4,
    Other = 5
}