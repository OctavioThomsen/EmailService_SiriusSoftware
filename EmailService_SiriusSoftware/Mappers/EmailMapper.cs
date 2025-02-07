using EmailService_SiriusSoftware.Dtos;
using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Mappers;
public static class EmailMapper
{
    public static EmailModel ToEmailModel(this EmailDto dto)
    {
        return new EmailModel
        {
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
            Sender = model.Sender,
            Recipient = model.Recipient,
            Body = model.Body
        };
    }
}

