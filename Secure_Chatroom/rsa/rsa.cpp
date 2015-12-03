// This is the main DLL file.

#include "stdafx.h"
#include <math.h>
#include <algorithm>
#include "rsa.h"

using namespace System::Collections::Generic;
using namespace Encryption;

ull Encryption::diffie_hellman::GeneratePrime(ull min, ull max)
{
	while (1)
	{
		ull randVal = ((ull)rand()) << 32 | rand();
		ull ret = (randVal % ((max - 1) - (min + 1))) + min;
		ull i = 2;

		if (ret % 2 == 0)
			ret++;

		for (; i < ret / 2; i++)
		{
			if (ret % i == 0)
				break;
		}

		if (i == ret / 2)
			return ret;
	}
	return 0;
}

long long ModInverse(long long a, long long b)
{
	long long b0 = b, t, q;
	long long x0 = 0, x1 = 1;

	if (b == 1) return 1;

	while (a > 1) {
		q = a / b;
		t = b, b = a % b, a = t;
		t = x0, x0 = x1 - q * x0, x1 = t;
	}

	if (x1 < 0) x1 += b0;

	return x1;
}

Keys ^ Encryption::diffie_hellman::KeyGen(ull p, ull q)
{
	ull m = 0;
	ull c = 0;
	ull t = 0;
	
	Keys ^ ret = gcnew Keys();

	//product of (p-1 * q-1)
	t = (p - 1) * (q - 1);
	//3120

	ret->public_key_e = GeneratePrime(10000, t);
	ret->public_key_n = p * q;

	//compute the private key, which is the mod inverse of e mod n
	ret->private_key = ModInverse(ret->public_key_e, t);
	
	return ret;
}

ull Encryption::rsa::Encrypt(Keys ^ key, ull val_to_convert)
{
	ull temp = 1;

	for (int p = 0; p < key->public_key_e; p++)
		temp = (temp * val_to_convert) % key->public_key_n;

	return temp;
}

ull Encryption::rsa::Decrypt(Keys ^ key, ull cipher_val)
{
	ull temp = 1;

	for (int p = 0; p < key->private_key; p++)
		temp = (temp * cipher_val) % key->public_key_n;

	return temp;
}
