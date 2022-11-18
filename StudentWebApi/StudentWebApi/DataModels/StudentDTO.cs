﻿namespace StudentWebApi.DataModels
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public Guid GenderId { get; set; }

        //şimdi de Context Tarafı eşlemelerini de yapalım
        public GenderDTO? Gender { get; set; }   
        public AddressDTO? Address { get; set; }
    }
}
