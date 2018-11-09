using DataAccessLayer;
using IDataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DataAccessLayerTests
{
    [TestClass]
    public class RepositoryTests

    {
        [TestInitialize]
        public void Setup()
        {
            //DropDB();
        }

        [TestCleanup]
        public void Cleanup()
        {
           //DropDB();
        }

        private void DropDB()
        {
            MongoUrl url = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString);
            MongoClient client = new MongoClient(url);
            client.DropDatabase(url.DatabaseName);
        }


        [TestMethod]
        public void IntiateDataBase()
        {
            IRepository<PatientMedicalHistory> _PatientMedicalHistory = new Repository<PatientMedicalHistory>();
            for (int i = 0; i < 10; i++)
            {
                var PatientMedicalHistory = new PatientMedicalHistory()
                {
                    PatientName = "Ayman",
                    SocialNumber = "123456789",
                    Doctor = new Doctor() { ID = i.ToString(), Name = $"d{i}" },
                    Email = "amohamed@integrant.com",
                    Hospital = new Hospital() { ID = $"{i}", Name = $"h{i}" },
                    Medicines = new List<Medicine>() { new Medicine() { ID = "1", Name = "M1" }, new Medicine() { ID = "2", Name = "M2" } },
                    Surgeries = new List<Surgery>() { new Surgery() { ID = "1", Name = "S1" }, new Surgery() { ID = "2", Name = "S2" } }
                };

                _PatientMedicalHistory.Add(PatientMedicalHistory);
            }
        }

        [TestMethod]
        public void AddAndUpdateTest()
        {

            IRepository<Customer> _customerRepo = new Repository<Customer>();
            IRepositoryManager<Customer> _customerMan = new RepositoryManager<Customer>();

            Assert.IsFalse(_customerMan.Exists);

            Customer customer = new Customer
            {
                FirstName = "Bob",
                LastName = "Dillon",
                Phone = "0900999899",
                Email = "Bob.dil@snailmail.com",
                HomeAddress = new Address
                {
                    Address1 = "North kingdom 15 west",
                    Address2 = "1 north way",
                    PostCode = "40990",
                    City = "George Town",
                    Country = "Alaska"
                }
            };

            _customerRepo.Add(customer);

            Assert.IsTrue(_customerMan.Exists);

            Assert.IsNotNull(customer.Id);

            // fetch it back 
            Customer alreadyAddedCustomer = _customerRepo.Where(c => c.FirstName == "Bob").Single();

            Assert.IsNotNull(alreadyAddedCustomer);
            Assert.AreEqual(customer.FirstName, alreadyAddedCustomer.FirstName);
            Assert.AreEqual(customer.HomeAddress.Address1, alreadyAddedCustomer.HomeAddress.Address1);

            alreadyAddedCustomer.Phone = "10110111";
            alreadyAddedCustomer.Email = "dil.bob@fastmail.org";

            _customerRepo.Update(alreadyAddedCustomer);

            // fetch by id now 
            Customer updatedCustomer = _customerRepo.GetById(customer.Id);

            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual(alreadyAddedCustomer.Phone, updatedCustomer.Phone);
            Assert.AreEqual(alreadyAddedCustomer.Email, updatedCustomer.Email);

            Assert.IsTrue(_customerRepo.Exists(c => c.HomeAddress.Country == "Alaska"));
        }

        [TestMethod]
        public void ComplexEntityTest()
        {
            IRepository<Customer> _customerRepo = new Repository<Customer>();
            IRepository<Product> _productRepo = new Repository<Product>();

            Customer customer = new Customer
            {
                FirstName = "Erik",
                LastName = "Swaun",
                Phone = "123 99 8767",
                Email = "erick@mail.com",
                HomeAddress = new Address
                {
                    Address1 = "Main bulevard",
                    Address2 = "1 west way",
                    PostCode = "89560",
                    City = "Tempare",
                    Country = "Arizona"
                }
            };

            Order order = new Order
            {
                PurchaseDate = DateTime.Now.AddDays(-2)
            };
            List<OrderItem> orderItems = new List<OrderItem>();

            Product shampoo = _productRepo.Add(new Product() { Name = "Palmolive Shampoo", Price = 5 });
            Product paste = _productRepo.Add(new Product() { Name = "Mcleans Paste", Price = 4 });


            OrderItem item1 = new OrderItem { Product = shampoo, Quantity = 1 };
            OrderItem item2 = new OrderItem { Product = paste, Quantity = 2 };

            orderItems.Add(item1);
            orderItems.Add(item2);

            order.Items = orderItems;

            customer.Orders = new List<Order>
            {
                order
            };

            _customerRepo.Add(customer);

            Assert.IsNotNull(customer.Id);
            Assert.IsNotNull(customer.Orders[0].Items[0].Product.Id);

            // get the orders  
            List<IList<Order>> theOrders = _customerRepo.Where(c => c.Id == customer.Id).Select(c => c.Orders).ToList();
            IEnumerable<IList<OrderItem>> theOrderItems = theOrders[0].Select(o => o.Items);

            Assert.IsNotNull(theOrders);
            Assert.IsNotNull(theOrderItems);
        }


        [TestMethod]
        public void BatchTest()
        {
            IRepository<Customer> _customerRepo = new Repository<Customer>();

            List<Customer> custlist = new List<Customer>(new Customer[] {
                new Customer() { FirstName = "Customer A" },
                new Customer() { FirstName = "Client B" },
                new Customer() { FirstName = "Customer C" },
                new Customer() { FirstName = "Client D" },
                new Customer() { FirstName = "Customer E" },
                new Customer() { FirstName = "Client F" },
                new Customer() { FirstName = "Customer G" },
            });

            //Insert batch
            _customerRepo.Add(custlist);

            long count = _customerRepo.Count();
            Assert.AreEqual(7, count);
            foreach (Customer c in custlist)
            {
                Assert.AreNotEqual(new string('0', 24), c.Id);
            }

            //Update batch
            foreach (Customer c in custlist)
            {
                c.LastName = c.FirstName;
            }

            _customerRepo.Update(custlist);

            foreach (Customer c in _customerRepo)
            {
                Assert.AreEqual(c.FirstName, c.LastName);
            }

            //Delete by criteria
            _customerRepo.Delete(f => f.FirstName.StartsWith("Client"));

            count = _customerRepo.Count();
            Assert.AreEqual(4, count);

            //Delete specific object
            _customerRepo.Delete(custlist[0]);

            //Test AsQueryable
            IQueryable<Customer> selectedcustomers = from cust in _customerRepo
                                                     where cust.LastName.EndsWith("C") || cust.LastName.EndsWith("G")
                                                     select cust;

            Assert.AreEqual(2, selectedcustomers.ToList().Count);

            count = _customerRepo.Count();
            Assert.AreEqual(3, count);

            //Drop entire repo
            new RepositoryManager<Customer>().Drop();

            count = _customerRepo.Count();
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void CollectionNamesTest()
        {
            Repository<Animal> a = new Repository<Animal>();
            RepositoryManager<Animal> am = new RepositoryManager<Animal>();
            Dog va = new Dog();
            Assert.IsFalse(am.Exists);
            a.Update(va);
            Assert.IsTrue(am.Exists);
            Assert.IsInstanceOfType(a.GetById(va.Id), typeof(Dog));
            Assert.AreEqual(am.Name, "AnimalsTest");
            Assert.AreEqual(a.CollectionName, "AnimalsTest");

            Repository<CatLike> cl = new Repository<CatLike>();
            RepositoryManager<CatLike> clm = new RepositoryManager<CatLike>();
            Lion vcl = new Lion();
            Assert.IsFalse(clm.Exists);
            cl.Update(vcl);
            Assert.IsTrue(clm.Exists);
            Assert.IsInstanceOfType(cl.GetById(vcl.Id), typeof(Lion));
            Assert.AreEqual(clm.Name, "Catlikes");
            Assert.AreEqual(cl.CollectionName, "Catlikes");

            Repository<Bird> b = new Repository<Bird>();
            RepositoryManager<Bird> bm = new RepositoryManager<Bird>();
            Bird vb = new Bird();
            Assert.IsFalse(bm.Exists);
            b.Update(vb);
            Assert.IsTrue(bm.Exists);
            Assert.IsInstanceOfType(b.GetById(vb.Id), typeof(Bird));
            Assert.AreEqual(bm.Name, "Birds");
            Assert.AreEqual(b.CollectionName, "Birds");

            Repository<Lion> l = new Repository<Lion>();
            RepositoryManager<Lion> lm = new RepositoryManager<Lion>();
            Lion vl = new Lion();
            //Assert.IsFalse(lm.Exists);   //Should already exist (created by cl)
            l.Update(vl);
            Assert.IsTrue(lm.Exists);
            Assert.IsInstanceOfType(l.GetById(vl.Id), typeof(Lion));
            Assert.AreEqual(lm.Name, "Catlikes");
            Assert.AreEqual(l.CollectionName, "Catlikes");

            Repository<Dog> d = new Repository<Dog>();
            RepositoryManager<Dog> dm = new RepositoryManager<Dog>();
            Dog vd = new Dog();
            //Assert.IsFalse(dm.Exists);
            d.Update(vd);
            Assert.IsTrue(dm.Exists);
            Assert.IsInstanceOfType(d.GetById(vd.Id), typeof(Dog));
            Assert.AreEqual(dm.Name, "AnimalsTest");
            Assert.AreEqual(d.CollectionName, "AnimalsTest");

            Repository<Bird> m = new Repository<Bird>();
            RepositoryManager<Bird> mm = new RepositoryManager<Bird>();
            Macaw vm = new Macaw();
            //Assert.IsFalse(mm.Exists);
            m.Update(vm);
            Assert.IsTrue(mm.Exists);
            Assert.IsInstanceOfType(m.GetById(vm.Id), typeof(Macaw));
            Assert.AreEqual(mm.Name, "Birds");
            Assert.AreEqual(m.CollectionName, "Birds");

            Repository<Whale> w = new Repository<Whale>();
            RepositoryManager<Whale> wm = new RepositoryManager<Whale>();
            Whale vw = new Whale();
            Assert.IsFalse(wm.Exists);
            w.Update(vw);
            Assert.IsTrue(wm.Exists);
            Assert.IsInstanceOfType(w.GetById(vw.Id), typeof(Whale));
            Assert.AreEqual(wm.Name, "Whale");
            Assert.AreEqual(w.CollectionName, "Whale");
        }


        [TestMethod]
        public void CustomIDTypeTest()
        {
            Repository<IntCustomer, int> xint = new Repository<IntCustomer, int>
            {
                new IntCustomer() { Id = 1, Name = "Test A" },
                new IntCustomer() { Id = 2, Name = "Test B" }
            };

            IntCustomer yint = xint.GetById(2);
            Assert.AreEqual(yint.Name, "Test B");

            xint.Delete(2);
            Assert.AreEqual(1, xint.Count());
        }


        public abstract class BaseItem : Entity
        {
            public string Id { get; set; }
        }

        public abstract class BaseA : BaseItem
        { }

        public class SpecialA : BaseA
        { }

        [TestMethod]
        public void Discussion433878()
        {
            Repository<SpecialA> specialRepository = new Repository<SpecialA>();
        }

        public abstract class ClassA : Entity
        {
            public string Prop1 { get; set; }
        }

        public class ClassB : ClassA
        {
            public string Prop2 { get; set; }
        }

        public class ClassC : ClassA
        {
            public string Prop3 { get; set; }
        }

        [TestMethod]
        public void Discussion572382()
        {
            Repository<ClassA> repo = new Repository<ClassA>() {
                new ClassB() { Prop1 = "A", Prop2 = "B" } ,
                new ClassC() { Prop1 = "A", Prop3 = "C" }
            };

            Assert.AreEqual(2, repo.Count());

            Assert.AreEqual(2, repo.OfType<ClassA>().Count());
            Assert.AreEqual(1, repo.OfType<ClassB>().Count());
            Assert.AreEqual(1, repo.OfType<ClassC>().Count());
        }

    }
}
