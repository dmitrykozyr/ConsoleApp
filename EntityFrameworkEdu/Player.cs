namespace EntityFrameworkEdu
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Навигационное св-во
        public User User { get; set; }
    }
}
