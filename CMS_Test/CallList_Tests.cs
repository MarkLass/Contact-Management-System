using Contact_Management_System.EndpointDefinitions;
using Contact_Management_System.Models;
using Contact_Management_System.Repositories;

namespace CMS_Test
{
    [TestClass]
    public class CallList_Tests
    {
        [TestMethod]
        public void Test_Should_Be_1()
        {
            LiteDbContext dbContext = new();
            var contact = new Contact
            {
                id = 0,
                name = new ContactName { first = "TestFirst", last = "TestLast", middle = "TestMiddle" },
                address = new ContactAddress { street = "1 Main St", city = "AnyTown", state = "Va", zipCode = "12345" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "123-456-7890", type = PhoneType.home } }
            };

            var create = ContactEndpointDefinition.CreateContact(contact, dbContext);

            var result = ContactEndpointDefinition.GetContactCallList(dbContext);

            var content = Helper.GetResponseValue<List<ContactCallList>>(result);

            Assert.IsNotNull(content.Result);
            Assert.AreEqual(1, actual: content.Result.Count());

            dbContext.Database.Dispose();
        }

        [TestMethod]
        public void Test_Should_Be_NotFound()
        {
            LiteDbContext dbContext = new();
            var contact = new Contact
            {
                id = 0,
                name = new ContactName { first = "TestFirst", last = "TestLast", middle = "TestMiddle" },
                address = new ContactAddress { street = "1 Main St", city = "AnyTown", state = "Va", zipCode = "12345" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "123-456-7890", type = PhoneType.mobile } }
            };

            var create = ContactEndpointDefinition.CreateContact(contact, dbContext);

            var result = ContactEndpointDefinition.GetContactCallList(dbContext);
            
            Assert.AreEqual("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult", result.ToString());

            dbContext.Database.Dispose();
        }

        [TestMethod]
        public void Test_Should_Be_1_With_Home_Phone()
        {
            LiteDbContext dbContext = new();
            var contact = new Contact
            {
                id = 0,
                name = new ContactName { first = "TestFirst", last = "TestLast", middle = "TestMiddle" },
                address = new ContactAddress { street = "1 Main St", city = "AnyTown", state = "Va", zipCode = "12345" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "123-456-7890", type = PhoneType.home },
                                                       new ContactPhone { number = "123-456-1111", type = PhoneType.mobile }  }
            };

            var create = ContactEndpointDefinition.CreateContact(contact, dbContext);

            var result = ContactEndpointDefinition.GetContactCallList(dbContext);

            var content = Helper.GetResponseValue<List<ContactCallList>>(result);

            Assert.IsNotNull(content.Result);
            Assert.AreEqual(1, actual: content.Result.Count());
            Assert.IsNotNull(content.Result[0]);
            Assert.AreEqual("123-456-7890", content.Result[0].phone);

            dbContext.Database.Dispose();
        }

        [TestMethod]
        public void Test_Should_Be_1_With_2_Contacts()
        {
            LiteDbContext dbContext = new();
            ContactEndpointDefinition.CreateContact(new Contact
            {
                id = 1,
                name = new ContactName { first = "TestFirst", last = "TestLast", middle = "TestMiddle" },
                address = new ContactAddress { street = "1 Main St", city = "AnyTown", state = "Va", zipCode = "12345" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "123-456-7890", type = PhoneType.home },
                                                   new ContactPhone { number = "123-456-1111", type = PhoneType.mobile } }
            }, dbContext);
            ContactEndpointDefinition.CreateContact(new Contact
            {
                id = 2,
                name = new ContactName { first = "TestFirst2", last = "TestLast2", middle = "TestMiddle2" },
                address = new ContactAddress { street = "2 Main St", city = "AnyTown", state = "Va", zipCode = "23456" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "456-456-7890", type = PhoneType.work },
                                                   new ContactPhone { number = "456-456-1111", type = PhoneType.mobile } }
            }, dbContext);

            var result = ContactEndpointDefinition.GetContactCallList(dbContext);

            var content = Helper.GetResponseValue<List<ContactCallList>>(result);

            Assert.IsNotNull(content.Result);
            Assert.AreEqual(1, actual: content.Result.Count());
            Assert.IsNotNull(content.Result[0]);
            Assert.AreEqual("123-456-7890", content.Result[0].phone);

            dbContext.Database.Dispose();
        }

        [TestMethod]
        public void Test_Should_Be_2_With_3_Contacts()
        {
            LiteDbContext dbContext = new();

            ContactEndpointDefinition.CreateContact(new Contact
            {
                id = 1,
                name = new ContactName { first = "TestFirst", last = "TestLast", middle = "TestMiddle" },
                address = new ContactAddress { street = "1 Main St", city = "AnyTown", state = "Va", zipCode = "12345" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "123-456-7890", type = PhoneType.home },
                                                   new ContactPhone { number = "123-456-1111", type = PhoneType.mobile } }
            }, dbContext);
            ContactEndpointDefinition.CreateContact(new Contact
            {
                id = 2,
                name = new ContactName { first = "TestFirst2", last = "TestLast2", middle = "TestMiddle2" },
                address = new ContactAddress { street = "2 Main St", city = "AnyTown", state = "Va", zipCode = "23456" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "456-456-7890", type = PhoneType.work },
                                                   new ContactPhone { number = "456-456-1111", type = PhoneType.mobile } }
            }, dbContext);

            ContactEndpointDefinition.CreateContact(new Contact
            {
                id = 3,
                name = new ContactName { first = "TestFirst3", last = "TestLast3", middle = "TestMiddle3" },
                address = new ContactAddress { street = "3 Main St", city = "AnyTown", state = "Va", zipCode = "33345" },
                email = "testEmail@company.com",
                phone = new List<ContactPhone>() { new ContactPhone { number = "333-456-7890", type = PhoneType.home },
                                                   new ContactPhone { number = "333-456-1111", type = PhoneType.mobile } }
            }, dbContext);

            var result = ContactEndpointDefinition.GetContactCallList(dbContext);

            var content = Helper.GetResponseValue<List<ContactCallList>>(result);

            Assert.IsNotNull(content.Result);
            Assert.AreEqual(2, actual: content.Result.Count());
            Assert.IsNotNull(content.Result[0]);
            Assert.AreEqual("123-456-7890", content.Result[0].phone);
            Assert.AreEqual("333-456-7890", content.Result[1].phone);

            dbContext.Database.Dispose();
        }
    }
}