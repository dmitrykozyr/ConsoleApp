namespace EntityFrameworkEdu
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public User User { get; set; }  // Навигационное свойствово
    }
}
