// rsa.h

#pragma once

using namespace System;
using namespace System::Collections::Generic;

namespace Encryption {
	typedef unsigned long long ull;
	typedef unsigned char chunk_type;

	public ref struct Keys
	{
		ull public_key_e;
		ull public_key_n;
		ull private_key;
	};

	public ref class rsa
	{
	public:
		ull Decrypt(Keys ^ key, ull cipher_val);
		ull Encrypt(Keys ^ key, ull val_to_convert);
	};

	public ref class diffie_hellman
	{
	public:
		///<summary>p is prime 1, q is prime 2, returns public and private keys in <c>keys</c> struct</summary>
		static Keys ^ KeyGen(ull p, ull q);
		static ull GeneratePrime(ull min, ull max);

	
		// TODO: Add your methods for this class here.
	};


}


