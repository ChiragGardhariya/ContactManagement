
using AutoMapper;
using ContactAPI.Models;
using ContactAPI.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactAPI.Manager
{
	public class RepoContact : IContact
	{


		public int CreateContact(ContactVM.AddContactInput input)
		{

			ValidateUser(input); //mobile number uniqueness

			try
			{
				Mapper.CreateMap<ContactVM.AddContactInput, Contact>();
				var contact = Mapper.Map<ContactVM.AddContactInput, Contact>(input);
				contact.Status = true;

				using (DB_A6BB95_ContactManageEntities context = new DB_A6BB95_ContactManageEntities())
				{
					contact.CreatedTimeIST = CommonFunctionality.GetISTTime();
					context.Contacts.Add(contact);
					context.SaveChanges();
					return contact.Id;
				}
			}
			catch (Exception ex)
			{
				throw;
			}


		  
		}

		public void EditContact(ContactVM.EditContactInput input)
		{
			ValidateUser(input, input.Id);
			try
			{
				Mapper.CreateMap<ContactVM.EditContactInput, Contact>();
				var contact = Mapper.Map<ContactVM.EditContactInput, Contact>(input);				

				using (DB_A6BB95_ContactManageEntities context = new DB_A6BB95_ContactManageEntities())
				{
					contact.LastUpdatedTimeIST = CommonFunctionality.GetISTTime();
					context.Entry(contact).State = System.Data.Entity.EntityState.Modified;
					context.SaveChanges();
					
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public Contact GetContactById(int id)
		{
			using(DB_A6BB95_ContactManageEntities context = new DB_A6BB95_ContactManageEntities())
			{
			   return context.Contacts.Find(id);				
			}

		
		}

		public List<Contact> GetContacts(bool? status)
		{
			using (DB_A6BB95_ContactManageEntities context = new DB_A6BB95_ContactManageEntities())
			{
				return status.HasValue ? context.Contacts.Where(x => x.Status == status).ToList() : context.Contacts.ToList();
			}
			
		}

		public void ChangeStatus(int cid)
		{
			using (DB_A6BB95_ContactManageEntities context = new DB_A6BB95_ContactManageEntities())
			{
				var contact = context.Contacts.Find(cid);

				if (contact == null)
				{
					throw new Exception("Given contact does not exist");
				}
				contact.LastUpdatedTimeIST = CommonFunctionality.GetISTTime();
				contact.Status = !contact.Status;
				context.Entry(contact).State = System.Data.Entity.EntityState.Modified;
				context.SaveChanges();
			}

		}

		private void ValidateUser(ContactVM.AddContactInput input, int id = 0)
		{
			//here we consider that mobile number must be unique.
			using (DB_A6BB95_ContactManageEntities context = new DB_A6BB95_ContactManageEntities())
			{
			   if(context.Contacts.Any(x=>x.PhoneNumber.Equals(input.PhoneNumber) && x.Id != id))
				{
					throw new Exception("User with "+input.PhoneNumber+" is already registered with us");
				}
			}

		}
	}
}