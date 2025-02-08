using EmailService_SiriusSoftware.Dtos;
using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Mappers;
public static class EmailMapper
{
    public static EmailModel ToEmailModel(this EmailDto dto)
    {
        return new EmailModel
        {
            IdEmail = dto.IdEmail,
            Sender = dto.Sender,
            Recipient = dto.Recipient,
            Body = dto.Body,
            SendStatus = "Pending"
        };
    }

    public static EmailDto ToEmailDto(this EmailModel model)
    {
        return new EmailDto
        {
            IdEmail = model.IdEmail,
            Sender = model.Sender ?? string.Empty,
            Recipient = model.Recipient ?? string.Empty,
            Body = model.Body ?? string.Empty
        };
    }

    public static EmailModel ToEmailModel(this EmailRequestDto dto)
    {
        return new EmailModel
        {
            Sender = "oti_thomsen98@hotmail.com",
            Recipient = dto.Recipient,
            Body = dto.Body,
            SendStatus = "Pending"
        };
    }
}

