using Application.Services.MernisService;
using Core.CrossCuttingConcerns.Exceptions.Types;
using NVIMernisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.MernisService;
public class NVIMernisServiceAdapter : MernisServiceBase
{
    public override async Task CheckIfRealPerson(long nationaltyId, string firstName, string lastName, int yearOfBirth)
    {
        KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
        var result = client.TCKimlikNoDogrulaAsync(nationaltyId, firstName.ToUpper(), lastName.ToUpper(), yearOfBirth).Result.Body.TCKimlikNoDogrulaResult;

        if (!result)
        {
            throw new BusinessException("Invalid person!");
        }

        await Task.CompletedTask;
    }
}
