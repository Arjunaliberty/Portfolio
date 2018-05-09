using Core.Context;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Concrete
{
    /// <summary>
    /// Класс реализующий инетрефейс IManagement
    /// </summary>
    public static class UserManagement
    {
        public static User Get(int id)
        {
            User result;

            using(DatabaseContext db = new DatabaseContext())
            {
                var temp = db.Users.Include(u => u.Department).Where(u => u.Id == id).FirstOrDefault();
               
                result = temp ?? throw new Exception("Записей в БД не существует");

            }

            return result;
        }

        public static List<User> GetAll()
        {
            List<User> result = null;

            using (DatabaseContext db = new DatabaseContext())
            {
                var list = db.Users.Include(u => u.Department).ToList();

                result = list ?? throw new Exception("Записей в БД не существует");
            }

            return result; 
        }

        public static void Add(User user)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                if(user != null)
                {
                    db.Entry(user).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
        }

        public static void Update(User user)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                if (user != null)
                {
                    var temp = db.Users.Include(u => u.Department).Where(u => u.Id == user.Id).FirstOrDefault();
                    if(temp != null)
                    {
                        temp.UserName = user.UserName;
                        temp.Department.Title = user.Department.Title;
                        db.SaveChanges();
                    }
                }
            }
        }

        public static void Delete(User user)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var temp = db.Users.Include(u=>u.Department).Where(u => u.Id == user.Id).FirstOrDefault();
                if (temp != null)
                {
                    db.Users.Remove(temp);
                    db.SaveChanges();
                }
            }
        }
    }
}
