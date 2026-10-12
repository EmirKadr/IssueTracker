using IssueTracker.Api.Services;
using IssueTracker.Api.Dtos;
using IssueTracker.Api.Models;
namespace IssueTracker.Api.Validators;
    public class TicketValidator
    {

        //public static Dictionary<string, string[]> ValidateTicket(int id, UpdateTicketRequest request)
        //{
        //    var errors = ValidateApartment(new CreateTicketRequest
        //    {
        //        ApartmentNumber = request.ApartmentNumber,
        //        NumberOfRooms = request.NumberOfRooms,
        //        LivingArea = request.LivingArea,
        //        MonthlyRent = request.MonthlyRent,
        //        PropertyId = request.PropertyId,
        //    });
        //    if (id <= 0)
        //        errors["Id"] = ["ID måste vara större än 0."];
        //    return errors;
        //}

        public static Dictionary<string, string[]> ValidateTicket(UpdateTicketRequest request)
        {
            var errors = ValidateTicket(new CreateTicketRequest
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority
            });

            if (request.Status != "Open" && request.Status != "InProgress" && request.Status != "Closed")
            {
                errors["Status"] = ["Status får endast vara 'Open', 'InProgress' eller 'Closed'."];
            }

            return errors;
        }

        public static Dictionary<string, string[]> ValidateTicket(
            CreateTicketRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                errors["Title"] = ["Titel måste anges."];
            }
            else if (request.Title.Length < 5 || request.Title.Length > 100)
            {
                errors["Title"] = ["Titel måste vara mellan 5 och 100 tecken."];
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
            errors["Description"] = ["Beskrivning måste anges."];
            }
            else if (request.Description.Length < 10 || request.Description.Length > 1000)
            {
            errors["Description"] = ["Beskrivning måste vara mellan 10 och 1000 tecken."];
            }
            if (string.IsNullOrWhiteSpace(request.Priority))
            {
            errors["Priority"] = ["Prioritet måste anges."];
            }
            else if (request.Priority != "Low" && request.Priority != "Normal" && request.Priority != "High")
            {
            errors["Priority"] = ["Prioritet får endast vara 'Low', 'Normal' eller 'High'."];
            }



        return errors;
        }
    }


