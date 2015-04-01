package com.ocean;

import com.ocean.FileAdapter.ByteReadParser;
import com.ocean.FileAdapter.ByteWriteParser;

interface CoolHashBase {
	CoolHashException chex = new CoolHashException();
	ByteWriteParser bwp = DumpAdapter.getByteWriteParser();
	ByteReadParser brp = DumpAdapter.getByteReadParser();
	ConstantBit.Target ct = ConstantBit.Target.POINT;
	DumpAdapter dumpAdapter = new DumpAdapter("");
}