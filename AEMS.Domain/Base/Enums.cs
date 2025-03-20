namespace IMS.Domain.Base;

public enum LevelType
{
    Branch = 1,
    Category = 2
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Others = 3,
}

public enum BloodGroup
{
    A_Positive = 1,
    A_Negative = 2,
    B_Positive = 3,
    B_Negative = 4,
    AB_Positive = 5,
    AB_Negative = 6,
    O_Positive = 7,
    O_Negative = 8

}
public enum AcademicTerm
{
    Quarter = 1,
    Semester = 2,
    Year = 3
}

public enum AttendanceStatus
{
    Present = 1,
    Absent = 2,
    Leave = 3
}

public enum TemplateType
{
    SMS = 1,
    Whatsapp = 2,
    Email = 3,
    All = 4
}

public enum StockStatus
{
    Active = 1,
    Inactive = 2,
    Archived = 3
}


public enum MovementType
{
    IN = 1,
    OUT = 2
}

public enum ReorderStatus
{
    Pending,
    Ordered,
    Received
}