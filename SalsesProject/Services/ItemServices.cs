﻿using Microsoft.AspNetCore.Http.HttpResults;
using SalsesProject.Data;
using SalsesProject.Models;

namespace SalsesProject.Services
{
    public class ItemServices : IItemServices
    {
        private readonly ApplicationDbContext _context;
        public ItemServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Create(ItemsModel items)
        {
            if (items == null)
            {
                return 0;
            }
            _context.Items.Add(items);
            _context.SaveChanges();
            return 1;

        }

        public int Delete(int id)
        {
            var data = _context.Items.Find(id);
            if(data != null)
            {
                _context.Items.Remove(data);
                _context.SaveChanges();
                return 1;
            }
            return 0;  
        }

        public List<ItemsModel> GetAll()
        {
            var items = _context.Items.ToList();
            return items;
        }

        public ItemsModel GetById(int id)
        {
            var data = _context.Items.Find(id);
            return data;
        }

        public int Update(ItemsModel item)
        {
            _context.Items.Update(item);
            _context.SaveChanges();
            return 1;
        }
    }
}
