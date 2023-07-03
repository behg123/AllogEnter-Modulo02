using FluentValidation;

namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(c => c.Name)
        .NotEmpty()
        .WithMessage("You sould Fill Out a Name")
        .MaximumLength(50)
        .WithMessage("The {PropertyName} shouldn't have more than 50 characters");

        RuleFor(c => c.CNPJ)
        //Cascade(cascadeMode.Stop) <-- se der erro em uma validaçao, para imediatamente e retorna o erro em que parou
        .NotEmpty()
        .WithMessage("You sould Fill Out a CNPJ")
        .Length(14)
        .WithMessage("The CNPJ should have 14 characters")
        .Must(ValidaCNPJ)
        .When(c => c.CNPJ != null, ApplyConditionTo.CurrentValidator)
        .WithMessage("The CNPJ should be a valid number");

    }

    public bool ValidaCNPJ(string vrCNPJ)

    {

        string CNPJ = vrCNPJ.Replace(".", "");

        CNPJ = CNPJ.Replace("/", "");

        CNPJ = CNPJ.Replace("-", "");



        int[] digitos, soma, resultado;

        int nrDig;

        string ftmt;

        bool[] CNPJOk;



        ftmt = "6543298765432";

        digitos = new int[14];

        soma = new int[2];

        soma[0] = 0;

        soma[1] = 0;

        resultado = new int[2];

        resultado[0] = 0;

        resultado[1] = 0;

        CNPJOk = new bool[2];

        CNPJOk[0] = false;

        CNPJOk[1] = false;



        try

        {

            for (nrDig = 0; nrDig < 14; nrDig++)

            {

                digitos[nrDig] = int.Parse(

                    CNPJ.Substring(nrDig, 1));

                if (nrDig <= 11)

                    soma[0] += (digitos[nrDig] *

                      int.Parse(ftmt.Substring(

                      nrDig + 1, 1)));

                if (nrDig <= 12)

                    soma[1] += (digitos[nrDig] *

                      int.Parse(ftmt.Substring(

                      nrDig, 1)));

            }



            for (nrDig = 0; nrDig < 2; nrDig++)

            {

                resultado[nrDig] = (soma[nrDig] % 11);

                if ((resultado[nrDig] == 0) || (

                     resultado[nrDig] == 1))

                    CNPJOk[nrDig] = (

                    digitos[12 + nrDig] == 0);

                else

                    CNPJOk[nrDig] = (

                    digitos[12 + nrDig] == (

                    11 - resultado[nrDig]));

            }

            return (CNPJOk[0] && CNPJOk[1]);

        }

        catch

        {

            return false;

        }

    }
}
