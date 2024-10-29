using EzyBill.BLL.Interfaces;

namespace EzyBill.BLL
{
    public interface ICurrentUserContext<TUserKey>
    {
        public TUserKey CurrentUserId { get; set; }
        public string CurrentUsername { get; set; }
    }
    public class CurrentUserContext : ICurrentUserContext<string>
    {

        public string CurrentUserId { get; set; } = string.Empty;
        public string CurrentUsername { get; set; } = "EzyBill";
    }
}
