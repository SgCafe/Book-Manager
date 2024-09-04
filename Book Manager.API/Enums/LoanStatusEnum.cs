using System.ComponentModel;

namespace Book_Manager.API.Enums;

public enum LoanStatusEnum
{
    Available = 0,
    Unavailable = 1,
    Borrowed = 2,
    Returned = 3,
    None = 4
}