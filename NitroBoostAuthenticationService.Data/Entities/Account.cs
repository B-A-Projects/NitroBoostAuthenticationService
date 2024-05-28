using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NitroBoostAuthenticationService.Shared;
using NitroBoostAuthenticationService.Shared.Enums;

namespace NitroBoostAuthenticationService.Data.Entities;

public class Account
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    
    [Column("unique_username")]
    public string? UniqueUsername { get; set; }
    
    [Required]
    [Column("email")]
    public string Email { get; set; }
    
    [Required]
    [Column("password")]
    public byte[] Password { get; set; }
    
    [Required]
    [Column("salt")]
    public byte[] Salt { get; set; }
    
    [Required]
    [Column("role")]
    public Role UserRole { get; set; }
    
    [Required]
    [Column("verified")]
    [DefaultValue(false)]
    public bool Verified { get; set; }

    public Account() {}
    
    public AccountDto ToDto() => new AccountDto()
    {
        Id = Id,
        Email = Email,
        Password = Password,
        Salt = Salt,
        UniqueUsername = UniqueUsername,
        UserRole = UserRole
    };

    public AccountDto ToSafeDto() => new AccountDto()
    {
        Id = Id,
        Email = Email,
        UniqueUsername = UniqueUsername,
        UserRole = UserRole
    };
}