/**
 * 
 */
package com.ctfo.storage.dispatch.io;

import java.io.ByteArrayOutputStream;
import java.io.PrintWriter;


/**
 * @author zjhl
 *
 */
public class LineDelimiter {
    /** the line delimiter constant of the current O/S. */
    public static final LineDelimiter DEFAULT;

    /** Compute the default delimiter on he current OS */
    static {
        ByteArrayOutputStream bout = new ByteArrayOutputStream();
        PrintWriter out = new PrintWriter(bout, true);
        out.println();
        DEFAULT = new LineDelimiter(new String(bout.toByteArray()));
    }

    /**
     * A special line delimiter which is used for auto-detection of
     * EOL in {@link TextLineDecoder}.  If this delimiter is used,
     * {@link TextLineDecoder} will consider both  <tt>'\r'</tt> and
     * <tt>'\n'</tt> as a delimiter.
     */
    public static final LineDelimiter AUTO = new LineDelimiter("");

    /**
     * The CRLF line delimiter constant (<tt>"\r\n"</tt>)
     */
    public static final LineDelimiter CRLF = new LineDelimiter("\r\n");

    /**
     * The line delimiter constant of UNIX (<tt>"\n"</tt>)
     */
    public static final LineDelimiter UNIX = new LineDelimiter("\n");

    /**
     * The line delimiter constant of MS Windows/DOS (<tt>"\r\n"</tt>)
     */
    public static final LineDelimiter WINDOWS = CRLF;

    /**
     * The line delimiter constant of Mac OS (<tt>"\r"</tt>)
     */
    public static final LineDelimiter MAC = new LineDelimiter("\r");

    /**
     * The line delimiter constant for NUL-terminated text protocols
     * such as Flash XML socket (<tt>"\0"</tt>)
     */
    public static final LineDelimiter NUL = new LineDelimiter("\0");

    /** Stores the selected Line delimiter */
    private final String value;

    /**
     * Creates a new line delimiter with the specified <tt>value</tt>.
     */
    public LineDelimiter(String value) {
        if (value == null) {
            throw new IllegalArgumentException("delimiter");
        }

        this.value = value;
    }

    /**
     * Return the delimiter string.
     */
    public String getValue() {
        return value;
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public int hashCode() {
        return value.hashCode();
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }

        if (!(o instanceof LineDelimiter)) {
            return false;
        }

        LineDelimiter that = (LineDelimiter) o;

        return this.value.equals(that.value);
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public String toString() {
        if (value.length() == 0) {
            return "delimiter: auto";
        } else {
            StringBuilder buf = new StringBuilder();
            buf.append("delimiter:");

            for (int i = 0; i < value.length(); i++) {
                buf.append(" 0x");
                buf.append(Integer.toHexString(value.charAt(i)));
            }

            return buf.toString();
        }
    }
}
