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
    ///     Map revision model
    /// </summary>
    [DataContract]
    public class MapRevisionDto : IEquatable<MapRevisionDto>
    {
        /// <summary>
        ///     Gets or Sets Id
        /// </summary>
        [Required]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or Sets Map
        /// </summary>
        [Required]
        [DataMember(Name = "map", EmitDefaultValue = false)]
        public string Map { get; set; }

        /// <summary>
        ///     Gets or Sets ParentRevision
        /// </summary>
        [Required]
        [DataMember(Name = "parentRevision", EmitDefaultValue = true)]
        public int? ParentRevision { get; set; }

        /// <summary>
        ///     Returns true if MapRevisionDto instances are equal
        /// </summary>
        /// <param name="other">Instance of MapRevisionDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MapRevisionDto other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
                    Id.Equals(other.Id)
                ) &&
                (
                    Map == other.Map ||
                    Map != null &&
                    Map.Equals(other.Map)
                ) &&
                (
                    ParentRevision == other.ParentRevision ||
                    ParentRevision != null &&
                    ParentRevision.Equals(other.ParentRevision)
                );
        }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MapRevisionDto {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Map: ").Append(Map).Append("\n");
            sb.Append("  ParentRevision: ").Append(ParentRevision).Append("\n");
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
            return obj.GetType() == GetType() && Equals((MapRevisionDto) obj);
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

                hashCode = hashCode * 59 + Id.GetHashCode();
                if (Map != null)
                    hashCode = hashCode * 59 + Map.GetHashCode();
                if (ParentRevision != null)
                    hashCode = hashCode * 59 + ParentRevision.GetHashCode();
                return hashCode;
            }
        }

        #region Operators

#pragma warning disable 1591

        public static bool operator ==(MapRevisionDto left, MapRevisionDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MapRevisionDto left, MapRevisionDto right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591

        #endregion Operators
    }
}