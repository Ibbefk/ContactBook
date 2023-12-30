// ContactBook.cs
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class ContactBook
{
    private List<Contact> contacts;
    private string filePath = "contacts.json";

    public ContactBook()
    {
        LoadContacts();
    }

    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
        SaveContacts();
        Console.WriteLine($"Contact {contact.FirstName} {contact.LastName} added successfully.");
    }

    public void RemoveContact(string email)
    {
        Contact contactToRemove = contacts.Find(c => c.Email == email);
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            SaveContacts();
            Console.WriteLine($"Contact {contactToRemove.FirstName} {contactToRemove.LastName} removed successfully.");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }

    public void ListContacts()
    {
        Console.WriteLine("Contact List:");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName}, {contact.Email}");
        }
    }

    public void DisplayContactDetails(string email)
    {
        Contact contact = contacts.Find(c => c.Email == email);
        if (contact != null)
        {
            Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"Phone: {contact.PhoneNumber}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Address: {contact.Address}");
            Console.WriteLine($"Additional Info: Nickname - {contact.Nickname}, Birthday - {contact.Birthday}");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }

    public void LoadContacts()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
        }
        else
        {
            contacts = new List<Contact>();
        }
    }

    public void SaveContacts()
    {
        string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
