# AutoMapper Utils

When creating a new project, it is often necessary to configure mappings between similar classes :

Source class:

```
public class Origin
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname {get; set;}
}
```

Destination class:

```
public class Destination
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname {get; set;}
}
```

To configure this mapping using [AutoMapper](https://automapper.org/):
<br />
`var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());`

The problem is that the number of classes to map can quickly increase and configuration can become slow.

## Using AutoMapper Utils:

To use Using AutoMapper Utils, just inherit the class from MapFrom:

```
public class Destination : MapFrom<Origin>
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname {get; set;}
}
```

Then you have to configure the profile:

```
    profile.ConfigureMapping(Assembly.GetExecutingAssembly());
```

For more details on how to use the library, please visit the (project repository)[https://github.com/DotLand-Inc/AutoMapper-Utils].
