#pragma once

#include <cassert>

// Use (void) to silent unused warnings.
#define ASSERT(exp)                                     assert(exp)
#define ASSERT_M(exp, msg)                              assert(((void)msg, exp))