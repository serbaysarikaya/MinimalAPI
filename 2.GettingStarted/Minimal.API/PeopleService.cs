namespace Minimal.API;

public record Person(string fullName );
public class PeopleService
{

    private readonly List<Person> _people = new()
    {
        new Person("Serbay sarıkaya"),
        new Person("Fatma sarıkaya"),
        new Person("Emrah sarıkaya")
    };

    public IEnumerable<Person> Search(string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return _people;
        }
        return _people.Where(p => p.fullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }
}
