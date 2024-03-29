/*
 * CodeCharacter API
 *
 * Specification of the CodeCharacter API
 *
 * The version of the OpenAPI document: 2022.0.1
 * Contact: delta@nitt.edu
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace CodeCharacter.CoreLibrary.Models;

/// <summary>
///     Register user request
/// </summary>
[DataContract]
public class RegisterUserRequestDto : IEquatable<RegisterUserRequestDto>
{
    /// <summary>
    ///     Gets or Sets Username
    /// </summary>
    [Required]
    [DataMember(Name = "username", EmitDefaultValue = false)]
    public string Username { get; set; }

    /// <summary>
    ///     Gets or Sets Name
    /// </summary>
    [Required]
    [DataMember(Name = "name", EmitDefaultValue = false)]
    public string Name { get; set; }

    /// <summary>
    ///     Gets or Sets Email
    /// </summary>
    [Required]
    [RegularExpression("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,6}")]
    [DataMember(Name = "email", EmitDefaultValue = false)]
    public string Email { get; set; }

    /// <summary>
    ///     Gets or Sets Password
    /// </summary>
    [Required]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,32}$")]
    [StringLength(32, MinimumLength = 8)]
    [DataMember(Name = "password", EmitDefaultValue = false)]
    public string Password { get; set; }

    /// <summary>
    ///     Gets or Sets PasswordConfirmation
    /// </summary>
    [Required]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,32}$")]
    [StringLength(32, MinimumLength = 8)]
    [DataMember(Name = "passwordConfirmation", EmitDefaultValue = false)]
    public string PasswordConfirmation { get; set; }

    /// <summary>
    ///     Gets or Sets Country
    /// </summary>
    [Required]
    [DataMember(Name = "country", EmitDefaultValue = false)]
    public string Country { get; set; }

    /// <summary>
    ///     Gets or Sets College
    /// </summary>
    [Required]
    [DataMember(Name = "college", EmitDefaultValue = false)]
    public string College { get; set; }

    /// <summary>
    ///     Gets or Sets AvatarId
    /// </summary>
    [Required]
    [DataMember(Name = "avatarId", EmitDefaultValue = false)]
    public int AvatarId { get; set; }

    /// <summary>
    ///     Returns true if RegisterUserRequestDto instances are equal
    /// </summary>
    /// <param name="other">Instance of RegisterUserRequestDto to be compared</param>
    /// <returns>Boolean</returns>
    public bool Equals(RegisterUserRequestDto other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return
            (
                Username == other.Username ||
                Username != null &&
                Username.Equals(other.Username)
            ) &&
            (
                Name == other.Name ||
                Name != null &&
                Name.Equals(other.Name)
            ) &&
            (
                Email == other.Email ||
                Email != null &&
                Email.Equals(other.Email)
            ) &&
            (
                Password == other.Password ||
                Password != null &&
                Password.Equals(other.Password)
            ) &&
            (
                PasswordConfirmation == other.PasswordConfirmation ||
                PasswordConfirmation != null &&
                PasswordConfirmation.Equals(other.PasswordConfirmation)
            ) &&
            (
                Country == other.Country ||
                Country != null &&
                Country.Equals(other.Country)
            ) &&
            (
                College == other.College ||
                College != null &&
                College.Equals(other.College)
            ) &&
            (
                AvatarId == other.AvatarId ||
                AvatarId.Equals(other.AvatarId)
            );
    }

    /// <summary>
    ///     Returns the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class RegisterUserRequestDto {\n");
        sb.Append("  Username: ").Append(Username).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  Email: ").Append(Email).Append("\n");
        sb.Append("  Password: ").Append(Password).Append("\n");
        sb.Append("  PasswordConfirmation: ").Append(PasswordConfirmation).Append("\n");
        sb.Append("  Country: ").Append(Country).Append("\n");
        sb.Append("  College: ").Append(College).Append("\n");
        sb.Append("  AvatarId: ").Append(AvatarId).Append("\n");
        sb.Append("}\n");
        return sb.ToString();
    }

    /// <summary>
    ///     Returns the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    /// <summary>
    ///     Returns true if objects are equal
    /// </summary>
    /// <param name="obj">Object to be compared</param>
    /// <returns>Boolean</returns>
    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((RegisterUserRequestDto)obj);
    }

    /// <summary>
    ///     Gets the hash code
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 41;
            // Suitable nullity checks etc, of course :)
            if (Username != null)
                hashCode = hashCode * 59 + Username.GetHashCode();
            if (Name != null)
                hashCode = hashCode * 59 + Name.GetHashCode();
            if (Email != null)
                hashCode = hashCode * 59 + Email.GetHashCode();
            if (Password != null)
                hashCode = hashCode * 59 + Password.GetHashCode();
            if (PasswordConfirmation != null)
                hashCode = hashCode * 59 + PasswordConfirmation.GetHashCode();
            if (Country != null)
                hashCode = hashCode * 59 + Country.GetHashCode();
            if (College != null)
                hashCode = hashCode * 59 + College.GetHashCode();

            hashCode = hashCode * 59 + AvatarId.GetHashCode();
            return hashCode;
        }
    }

    #region Operators

#pragma warning disable 1591

    public static bool operator ==(RegisterUserRequestDto left, RegisterUserRequestDto right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(RegisterUserRequestDto left, RegisterUserRequestDto right)
    {
        return !Equals(left, right);
    }

#pragma warning restore 1591

    #endregion Operators
}