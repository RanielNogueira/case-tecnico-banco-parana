using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBTech.Domain.Models;
[Table("ReceiveNews")]
public class ReceiveNews
{
	[Column("id")]
	[Key]
	public virtual int Id { get; set; }

	[Required(AllowEmptyStrings = false,ErrorMessage = "FullName is not valid!")]
	[MaxLength(250, ErrorMessage = "FullName limi to 250 characters!")]
	public string FullName { get; set; }

	[DataType(DataType.EmailAddress)]
	[MaxLength(250, ErrorMessage = "Email limi to 250 characters!")]
	[Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
	[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
	public string Email { get; set; }

	public ReceiveNews() { }

	public ReceiveNews(int Id, string FullName, string Email)
	{
		this.Id = Id;
		this.FullName = FullName;
		this.Email = Email;
	}
}
