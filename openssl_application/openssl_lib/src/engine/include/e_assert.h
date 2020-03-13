#pragma once

#include <version.h>
#include <cassert>

// Use (void) to silent unused warnings.
#if ENABLE_ASSERT
#define ASSERT(exp)                                     assert(exp)
#define ASSERT_M(exp, msg)                              assert(((void)msg, exp))
#else // ENABLE_ASSERT
#define ASSERT(exp)                                     
#define ASSERT_M(exp, msg)                              
#endif // ENABLE_ASSERT
