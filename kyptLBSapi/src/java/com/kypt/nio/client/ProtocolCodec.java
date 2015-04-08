/*******************************************************************************
 * @(#)IDecode.java 2008-11-19
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.nio.client;

import org.apache.mina.filter.codec.ProtocolCodecFactory;
import org.apache.mina.filter.codec.ProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolEncoder;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-11-19 下午03:17:20
 */
public class ProtocolCodec implements ProtocolCodecFactory {

    public ProtocolDecoder getDecoder() throws Exception {
        return null;
    }

    public ProtocolEncoder getEncoder() throws Exception {
        return null;
    }

}
