using eNotification.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace NotificationService.Models
{
    public static class CustomExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<Doctor> doctors)
        {
            return
                doctors.OrderBy(doctor => doctor.SecondName)
                      .Select(doctor =>
                          new SelectListItem
                          {
                              Text = doctor.SecondName,
                              Value = doctor.Id.ToString()
                          });
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            Regex regex = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return regex.Replace(input, String.Empty);
        }
    }
}