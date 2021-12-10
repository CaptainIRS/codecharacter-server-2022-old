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
///     Leaderboard entry model
/// </summary>
[DataContract]
public class LeaderboardEntryDto : IEquatable<LeaderboardEntryDto>
{
    /// <summary>
    ///     Gets or Sets User
    /// </summary>
    [Required]
    [DataMember(Name = "user", EmitDefaultValue = false)]
    public PublicUserDto User { get; set; }

    /// <summary>
    ///     Gets or Sets Stats
    /// </summary>
    [Required]
    [DataMember(Name = "stats", EmitDefaultValue = false)]
    public UserStatsDto Stats { get; set; }

    /// <summary>
    ///     Returns true if LeaderboardEntryDto instances are equal
    /// </summary>
    /// <param name="other">Instance of LeaderboardEntryDto to be compared</param>
    /// <returns>Boolean</returns>
    public bool Equals(LeaderboardEntryDto other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return
            (
                User == other.User ||
                User != null &&
                User.Equals(other.User)
            ) &&
            (
                Stats == other.Stats ||
                Stats != null &&
                Stats.Equals(other.Stats)
            );
    }

    /// <summary>
    ///     Returns the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class LeaderboardEntryDto {\n");
        sb.Append("  User: ").Append(User).Append("\n");
        sb.Append("  Stats: ").Append(Stats).Append("\n");
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
        return obj.GetType() == GetType() && Equals((LeaderboardEntryDto) obj);
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
            if (User != null)
                hashCode = hashCode * 59 + User.GetHashCode();
            if (Stats != null)
                hashCode = hashCode * 59 + Stats.GetHashCode();
            return hashCode;
        }
    }

    #region Operators

#pragma warning disable 1591

    public static bool operator ==(LeaderboardEntryDto left, LeaderboardEntryDto right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LeaderboardEntryDto left, LeaderboardEntryDto right)
    {
        return !Equals(left, right);
    }

#pragma warning restore 1591

    #endregion Operators
}