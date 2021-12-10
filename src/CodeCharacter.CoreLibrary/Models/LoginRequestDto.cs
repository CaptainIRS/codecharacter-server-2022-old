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
///     Login request
/// </summary>
[DataContract]
public class LoginRequestDto : IEquatable<LoginRequestDto>
{
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
    ///     Returns true if LoginRequestDto instances are equal
    /// </summary>
    /// <param name="other">Instance of LoginRequestDto to be compared</param>
    /// <returns>Boolean</returns>
    public bool Equals(LoginRequestDto other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return
            (
                Email == other.Email ||
                Email != null &&
                Email.Equals(other.Email)
            ) &&
            (
                Password == other.Password ||
                Password != null &&
                Password.Equals(other.Password)
            );
    }

    /// <summary>
    ///     Returns the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class LoginRequestDto {\n");
        sb.Append("  Email: ").Append(Email).Append("\n");
        sb.Append("  Password: ").Append(Password).Append("\n");
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
        return obj.GetType() == GetType() && Equals((LoginRequestDto)obj);
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
            if (Email != null)
                hashCode = hashCode * 59 + Email.GetHashCode();
            if (Password != null)
                hashCode = hashCode * 59 + Password.GetHashCode();
            return hashCode;
        }
    }

    #region Operators

#pragma warning disable 1591

    public static bool operator ==(LoginRequestDto left, LoginRequestDto right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LoginRequestDto left, LoginRequestDto right)
    {
        return !Equals(left, right);
    }

#pragma warning restore 1591

    #endregion Operators
}