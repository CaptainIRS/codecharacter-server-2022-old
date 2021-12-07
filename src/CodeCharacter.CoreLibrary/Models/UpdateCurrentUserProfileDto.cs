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
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace CodeCharacter.CoreLibrary.Models
{
    /// <summary>
    ///     Update current user profile request
    /// </summary>
    [DataContract]
    public class UpdateCurrentUserProfileDto : IEquatable<UpdateCurrentUserProfileDto>
    {
        /// <summary>
        ///     Gets or Sets Username
        /// </summary>
        [DataMember(Name = "username", EmitDefaultValue = false)]
        public string Username { get; set; }

        /// <summary>
        ///     Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or Sets Country
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        ///     Gets or Sets College
        /// </summary>
        [DataMember(Name = "college", EmitDefaultValue = false)]
        public string College { get; set; }

        /// <summary>
        ///     Returns true if UpdateCurrentUserProfileDto instances are equal
        /// </summary>
        /// <param name="other">Instance of UpdateCurrentUserProfileDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateCurrentUserProfileDto other)
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
                    Country == other.Country ||
                    Country != null &&
                    Country.Equals(other.Country)
                ) &&
                (
                    College == other.College ||
                    College != null &&
                    College.Equals(other.College)
                );
        }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UpdateCurrentUserProfileDto {\n");
            sb.Append("  Username: ").Append(Username).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  College: ").Append(College).Append("\n");
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
            return obj.GetType() == GetType() && Equals((UpdateCurrentUserProfileDto) obj);
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
                if (Country != null)
                    hashCode = hashCode * 59 + Country.GetHashCode();
                if (College != null)
                    hashCode = hashCode * 59 + College.GetHashCode();
                return hashCode;
            }
        }

        #region Operators

#pragma warning disable 1591

        public static bool operator ==(UpdateCurrentUserProfileDto left, UpdateCurrentUserProfileDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UpdateCurrentUserProfileDto left, UpdateCurrentUserProfileDto right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591

        #endregion Operators
    }
}