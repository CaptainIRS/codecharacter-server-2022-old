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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using CodeCharacter.CoreLibrary.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CodeCharacter.CoreLibrary.Models;

/// <summary>
///     Match model
/// </summary>
[DataContract]
public class MatchDto : IEquatable<MatchDto>
{
    /// <summary>
    ///     Gets or Sets MatchMode
    /// </summary>
    [TypeConverter(typeof(CustomEnumConverter<MatchModeEnum>))]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MatchModeEnum
    {
        /// <summary>
        ///     Enum SELF for SELF
        /// </summary>
        [EnumMember(Value = "SELF")] SELF = 1,

        /// <summary>
        ///     Enum AI for AI
        /// </summary>
        [EnumMember(Value = "AI")] AI = 2,

        /// <summary>
        ///     Enum PREVCOMMIT for PREV_COMMIT
        /// </summary>
        [EnumMember(Value = "PREV_COMMIT")] PREVCOMMIT = 3,

        /// <summary>
        ///     Enum MANUAL for MANUAL
        /// </summary>
        [EnumMember(Value = "MANUAL")] MANUAL = 4,

        /// <summary>
        ///     Enum AUTO for AUTO
        /// </summary>
        [EnumMember(Value = "AUTO")] AUTO = 5
    }


    /// <summary>
    ///     Gets or Sets MatchVerdict
    /// </summary>
    [TypeConverter(typeof(CustomEnumConverter<MatchVerdictEnum>))]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MatchVerdictEnum
    {
        /// <summary>
        ///     Enum PLAYER1 for PLAYER1
        /// </summary>
        [EnumMember(Value = "PLAYER1")] PLAYER1 = 1,

        /// <summary>
        ///     Enum PLAYER2 for PLAYER2
        /// </summary>
        [EnumMember(Value = "PLAYER2")] PLAYER2 = 2,

        /// <summary>
        ///     Enum TIE for TIE
        /// </summary>
        [EnumMember(Value = "TIE")] TIE = 3
    }

    /// <summary>
    ///     Gets or Sets Id
    /// </summary>
    [Required]
    [DataMember(Name = "id", EmitDefaultValue = false)]
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets Games
    /// </summary>
    [Required]
    [DataMember(Name = "games", EmitDefaultValue = false)]
    public List<GameDto> Games { get; set; }

    /// <summary>
    ///     Gets or Sets MatchMode
    /// </summary>
    [Required]
    [DataMember(Name = "matchMode", EmitDefaultValue = false)]
    public MatchModeEnum MatchMode { get; set; }

    /// <summary>
    ///     Gets or Sets MatchVerdict
    /// </summary>
    [Required]
    [MinLength(1)]
    [DataMember(Name = "matchVerdict", EmitDefaultValue = false)]
    public MatchVerdictEnum MatchVerdict { get; set; }

    /// <summary>
    ///     Gets or Sets CreatedAt
    /// </summary>
    [Required]
    [DataMember(Name = "createdAt", EmitDefaultValue = false)]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Gets or Sets User1
    /// </summary>
    [Required]
    [DataMember(Name = "user1", EmitDefaultValue = false)]
    public PublicUserDto User1 { get; set; }

    /// <summary>
    ///     Gets or Sets User2
    /// </summary>
    [Required]
    [DataMember(Name = "user2", EmitDefaultValue = false)]
    public PublicUserDto User2 { get; set; }

    /// <summary>
    ///     Returns true if MatchDto instances are equal
    /// </summary>
    /// <param name="other">Instance of MatchDto to be compared</param>
    /// <returns>Boolean</returns>
    public bool Equals(MatchDto other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return
            (
                Id == other.Id ||
                Id != null &&
                Id.Equals(other.Id)
            ) &&
            (
                Games == other.Games ||
                Games != null &&
                other.Games != null &&
                Games.SequenceEqual(other.Games)
            ) &&
            (
                MatchMode == other.MatchMode ||
                MatchMode.Equals(other.MatchMode)
            ) &&
            (
                MatchVerdict == other.MatchVerdict ||
                MatchVerdict.Equals(other.MatchVerdict)
            ) &&
            (
                CreatedAt == other.CreatedAt ||
                CreatedAt != null &&
                CreatedAt.Equals(other.CreatedAt)
            ) &&
            (
                User1 == other.User1 ||
                User1 != null &&
                User1.Equals(other.User1)
            ) &&
            (
                User2 == other.User2 ||
                User2 != null &&
                User2.Equals(other.User2)
            );
    }

    /// <summary>
    ///     Returns the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class MatchDto {\n");
        sb.Append("  Id: ").Append(Id).Append("\n");
        sb.Append("  Games: ").Append(Games).Append("\n");
        sb.Append("  MatchMode: ").Append(MatchMode).Append("\n");
        sb.Append("  MatchVerdict: ").Append(MatchVerdict).Append("\n");
        sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
        sb.Append("  User1: ").Append(User1).Append("\n");
        sb.Append("  User2: ").Append(User2).Append("\n");
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
        return obj.GetType() == GetType() && Equals((MatchDto)obj);
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
            if (Id != null)
                hashCode = hashCode * 59 + Id.GetHashCode();
            if (Games != null)
                hashCode = hashCode * 59 + Games.GetHashCode();

            hashCode = hashCode * 59 + MatchMode.GetHashCode();

            hashCode = hashCode * 59 + MatchVerdict.GetHashCode();
            if (CreatedAt != null)
                hashCode = hashCode * 59 + CreatedAt.GetHashCode();
            if (User1 != null)
                hashCode = hashCode * 59 + User1.GetHashCode();
            if (User2 != null)
                hashCode = hashCode * 59 + User2.GetHashCode();
            return hashCode;
        }
    }

    #region Operators

#pragma warning disable 1591

    public static bool operator ==(MatchDto left, MatchDto right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MatchDto left, MatchDto right)
    {
        return !Equals(left, right);
    }

#pragma warning restore 1591

    #endregion Operators
}