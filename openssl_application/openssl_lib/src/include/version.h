#pragma once

#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING    1

#define ENABLE_ASSERT                                           (1)
#define ENABLE_NLOG                                             (1)
#define ENABLE_SAFE_MEMORY                                      (1 && ENABLE_ASSERT)
