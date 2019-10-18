using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDayCareRS.MVC.DATA.EF/*.Metadata*/
{
    public class LocationMetadata
    {
        [Required]
        public int LocationID { get; set; }
        [Required]
        [Display(Name = "Daycare")]
        public string LocationName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Required]
        [Display(Name = "Reservation Limit")]
        public byte ReservationLimit { get; set; }
    }
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location
    {

    }

    public class OwnerAssetMetadata
    {
        [Required]
        public int OwnerAssetID { get; set; }
        [Required]
        [Display(Name = "Pup's Name")]
        public string AssetName { get; set; }
        [Required]
        [Display(Name = "Owner ID")]
        public string OwnerID { get; set; }
        [Required]
        [Display(Name = "Pup Photo")]
        public string AssetPhoto { get; set; }
        [Required]
        [Display(Name = "Special Notes")]
        public string SpecialNotes { get; set; }
        [Required]
        [Display(Name = "Approved Customer")]
        public bool IsActive { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText ="[N/A]")]
        [Display(Name ="Date Added")]
        public System.DateTime DateAdded { get; set; }
    }
    [MetadataType(typeof(OwnerAssetMetadata))]
    public partial class OwnerAsset
    {

    }

    public class ReservationMetadata
    {
        [Required]
        public int ReservationID { get; set; }
        [Required]
        public int LocationID { get; set; }
        [Required]
        [Display(Name = "Reservation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "[N/A]")]
        public System.DateTime ReservationDate { get; set; }
        [Required]
        [Display(Name = "Owner ID")]
        public int OwnerAssetID { get; set; }
    }
    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation
    {
      
    }


    public class UserDetailMetadata
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {
        [Display (Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
