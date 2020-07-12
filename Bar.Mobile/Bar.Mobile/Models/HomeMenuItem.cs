namespace Bar.Mobile.Models
{
    public enum MenuItemType
    {
        Narudzbe,
        MojeNarudzbe,
        Login
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
