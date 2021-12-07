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

namespace CodeCharacter.CoreLibrary.Models
{
    /// <summary>
    /// Update latest code request
    /// </summary>
    [DataContract]
    public class UpdateLatestCodeRequestDto : IEquatable<UpdateLatestCodeRequestDto>
    {
        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [Required]
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or Sets Lock
        /// </summary>
        [DataMember(Name = "lock", EmitDefaultValue = false)]
        public bool Lock { get; set; } = false;

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UpdateLatestCodeRequestDto {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Lock: ").Append(Lock).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((UpdateLatestCodeRequestDto)obj);
        }

        /// <summary>
        /// Returns true if UpdateLatestCodeRequestDto instances are equal
        /// </summary>
        /// <param name="other">Instance of UpdateLatestCodeRequestDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateLatestCodeRequestDto other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Code == other.Code ||
                    Code != null &&
                    Code.Equals(other.Code)
                ) &&
                (
                    Lock == other.Lock ||

                    Lock.Equals(other.Lock)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (Code != null)
                    hashCode = hashCode * 59 + Code.GetHashCode();

                hashCode = hashCode * 59 + Lock.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(UpdateLatestCodeRequestDto left, UpdateLatestCodeRequestDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UpdateLatestCodeRequestDto left, UpdateLatestCodeRequestDto right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
