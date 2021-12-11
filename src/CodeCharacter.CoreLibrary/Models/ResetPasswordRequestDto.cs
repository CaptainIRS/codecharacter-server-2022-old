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
///     Reset password request
/// </summary>
[DataContract]
public class ResetPasswordRequestDto : IEquatable<ResetPasswordRequestDto>
{
    /// <summary>
    ///     Gets or Sets Token
    /// </summary>
    [Required]
    [DataMember(Name = "token", EmitDefaultValue = false)]
    public string Token { get; set; }

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
    ///     Returns true if ResetPasswordRequestDto instances are equal
    /// </summary>
    /// <param name="other">Instance of ResetPasswordRequestDto to be compared</param>
    /// <returns>Boolean</returns>
    public bool Equals(ResetPasswordRequestDto other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return
            (
                Token == other.Token ||
                Token != null &&
                Token.Equals(other.Token)
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
            );
    }

    /// <summary>
    ///     Returns the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ResetPasswordRequestDto {\n");
        sb.Append("  Token: ").Append(Token).Append("\n");
        sb.Append("  Password: ").Append(Password).Append("\n");
        sb.Append("  PasswordConfirmation: ").Append(PasswordConfirmation).Append("\n");
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
        return obj.GetType() == GetType() && Equals((ResetPasswordRequestDto)obj);
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
            if (Token != null)
                hashCode = hashCode * 59 + Token.GetHashCode();
            if (Password != null)
                hashCode = hashCode * 59 + Password.GetHashCode();
            if (PasswordConfirmation != null)
                hashCode = hashCode * 59 + PasswordConfirmation.GetHashCode();
            return hashCode;
        }
    }

    #region Operators

#pragma warning disable 1591

    public static bool operator ==(ResetPasswordRequestDto left, ResetPasswordRequestDto right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ResetPasswordRequestDto left, ResetPasswordRequestDto right)
    {
        return !Equals(left, right);
    }

#pragma warning restore 1591

    #endregion Operators
}