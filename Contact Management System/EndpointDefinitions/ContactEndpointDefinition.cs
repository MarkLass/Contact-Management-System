using Contact_Management_System.Models;
using Contact_Management_System.Repositories;
using Contact_Management_System.Utility;

namespace Contact_Management_System.EndpointDefinitions
{
    public class ContactEndpointDefinition : IEndpointDefintion
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/contacts", GetAllContacts);

            app.MapGet("/contacts/{id}", GetContactById);

            app.MapGet("/contacts/call-list", GetContactCallList);

            app.MapPost("/contacts", CreateContact);

            app.MapPut("/contacts/{id}", UpdateContact);

            app.MapDelete("/contacts/{id}", DeleteContact);
        }

        public static IResult GetAllContacts(ILiteDbContext dbContext)
        {
            try
            {
                return Results.Ok(dbContext.Database.GetCollection<Contact>("Contact").FindAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult GetContactById(int id, ILiteDbContext dbContext)
        {
            try
            {
                var contact = dbContext.Database.GetCollection<Contact>("Contact").Find(x => x.id == id).FirstOrDefault();
                return contact is not null ? Results.Ok(contact) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult GetContactCallList (ILiteDbContext dbContext)
        {
            try
            {
                var callList = dbContext.Database.GetCollection<Contact>("Contact")
                                    .FindAll().Where(r => r.phone.Any(h => h.type == PhoneType.home))
                                                               .Select(o => new ContactCallList
                                                               {
                                                                   name = new ContactName
                                                                   { first = o.name.first, last = o.name.last, middle = o.name.middle },
                                                                   phone = o.phone.First(h => h.type == PhoneType.home).number
                                                               });
                if (callList.Any())
                    return Results.Ok(callList);
                else
                    return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult CreateContact(Contact contact, ILiteDbContext dbContext) 
        {
            try
            {
                dbContext.Database.GetCollection<Contact>("Contact").Insert(contact);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult UpdateContact(Contact updatedContact, ILiteDbContext dbContext)
        {
            try
            {
                var contacts = dbContext.Database.GetCollection<Contact>("Contact");
                var results = contacts.Find(x => x.id == updatedContact.id).FirstOrDefault();
                if (results == null)
                {
                    return Results.NotFound();
                }
                contacts.Update(updatedContact);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult DeleteContact(int id, ILiteDbContext dbContext)
        {
            try
            {
                dbContext.Database.GetCollection<Contact>("Contact").Delete(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<ILiteDbContext, LiteDbContext>();
        }
    }
}
