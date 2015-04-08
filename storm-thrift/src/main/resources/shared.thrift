
/**
 * This Thrift file can be included by other Thrift files that want to share
 * these definitions.
 */

namespace cpp com.ocean.socket.thrift.common
namespace d com.ocean.socket.thrift.common 
namespace java com.ocean.socket.thrift.common
namespace perl com.ocean.socket.thrift.common
namespace php com.ocean.socket.thrift.common

struct SharedStruct {
  1: i32 key
  2: string value
}

service SharedService {
  SharedStruct getStruct(1: i32 key)
}
