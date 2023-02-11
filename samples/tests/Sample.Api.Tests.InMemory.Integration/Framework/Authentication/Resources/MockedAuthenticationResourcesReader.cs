using System;
using System.IO;
using System.Security.Cryptography;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Authentication.Resources;

public static class MockedAuthenticationResourcesReader
{
    private static readonly Type readerType = typeof(MockedAuthenticationResourcesReader);

    public static RSA GetPrivateRsaKey()
    {
        var rsa = RSA.Create();

        var privateKeyString = ReadEmbeddedResource("mock-RsaPrivateKey.pem");

        rsa.ImportFromPem(privateKeyString);

        return rsa;
    }

    public static string GetJwks()
    {
        return ReadEmbeddedResource("mock-Jwks.json");
    }

    public static string GetOpenIdConfiguration()
    {
        return ReadEmbeddedResource("mock-openid-config.json");
    }

    private static string ReadEmbeddedResource(string name)
    {
        var resourceFullName = $"{readerType.Namespace}.{name}";
        using var stream = readerType.Assembly.GetManifestResourceStream(resourceFullName)!;
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}
