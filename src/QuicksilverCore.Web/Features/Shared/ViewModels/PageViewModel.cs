namespace QuicksilverCore.Web.Features.Shared.ViewModels;
public class PageViewModel<T> where T : PageData
{
    public T CurrentPage { get; set; }
}
