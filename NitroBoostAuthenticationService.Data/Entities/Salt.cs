using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NitroBoostAuthenticationService.Shared.Dtos;

namespace NitroBoostAuthenticationService.Data.Entities;

public class Salt
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    
    [Required]
    [Column("user_id")]
    public long UserId { get; set; }
    
    [Required]
    [Column("salt_bytes")]
    public byte[] SaltBytes { get; set; }
    
    public Salt() {}

    public SaltDto ToDto() => new SaltDto()
    {
        Id = Id,
        Salt = SaltBytes,
        UserId = UserId
    };
}