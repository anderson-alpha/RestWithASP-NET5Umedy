using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        //SERVE PARA MOCKAR VALOR
        //private volatile int count;
        private MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Person> FindAll()
        {
            //List<Person> persons = new List<Person>();
            //for (int i = 0; i <8;i++) 
            //{

            //    Person person = MockPerson(i);
            //    persons.Add(person);
            //}

            return _context.Persons.ToList();
        }

        //private Person MockPerson(int i)
        //{
        //    return new Person
        //    {
        //        Id = 1, //IncrementAndGet(),
        //        FirstName = "Person Name" + i,
        //        LastName = "Person Last Name" + i,
        //        Address = "Person Address" + i,
        //        Gender = "Person Male" + i
        //    };
        //}

        //private long IncrementAndGet()
        //{
        //    return Interlocked.Increment(ref count);
        //}

        public Person FindByID(long id)
        {
            //return new Person
            //{
            //    Id = 1, //IncrementAndGet(),
            //    FirstName = "Anderson",
            //    LastName = "Sousa",
            //    Address = "Rua Principal",
            //    Gender = "Male"
            //};

            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception) 
                {
                    throw;
                }
            }
            return person;
        }

        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
