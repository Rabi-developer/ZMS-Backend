using IMS.Domain.Base;

namespace IMS.Domain.Entities;

public class Attachment : GeneralBase
{
    public string Path { get; set; }
    public string Schema { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }

}
