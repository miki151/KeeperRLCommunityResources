# Compiler and Flags
CFLAGS = -Wall -std=c++17 -Wno-sign-compare -Wno-unused-variable \
         -Wno-shift-count-overflow -Wno-tautological-constant-out-of-range-compare \
         -Wno-mismatched-tags -ftemplate-depth=512 -Wmissing-field-initializers \
         -Wno-deprecated-declarations

ifndef GCC
GCC = g++ # Use g++ by default for MinGW64, or change to clang++ if you prefer
endif

CFLAGS += -g

OBJDIR = obj

NAME = umgl

# WebAssembly build configuration
ifdef WEBASM
GCC = em++
CFLAGS += -O3 -s EXPORTED_FUNCTIONS='["_get_result", "_main"]' \
           -s EXTRA_EXPORTED_RUNTIME_METHODS='["ccall", "cwrap"]' \
           -s ASSERTIONS=1 -s DISABLE_EXCEPTION_CATCHING=0
NAME = umgl.js
endif

CC = $(GCC)
LD = $(CC)

ROOT = ./
TOROOT = ./../
IPATH = -I.

CFLAGS += $(IPATH)

# MinGW64-specific library paths
LDFLAGS += -L/mingw64/lib

# Adjust the source file listing for Windows
SRCS = $(shell ls src/*.cpp 2>/dev/null)

INSTALL_DIR = $(shell pwd)
CFLAGS += "-DINSTALL_DIR=\"$(INSTALL_DIR)\""

# MinGW64-specific libraries, modify if required
LIBS = -L/mingw64/lib ${LDFLAGS}

OBJS = $(addprefix $(OBJDIR)/,$(notdir $(SRCS:.cpp=.o)))
DEPS = $(addprefix $(OBJDIR)/,$(notdir $(SRCS:.cpp=.d)))

##############################################################################

# Build process
all:
	@$(MAKE) --no-print-directory info
	@$(MAKE) --no-print-directory compile

compile: $(NAME)

$(OBJDIR)/%.o: src/%.cpp
	@mkdir -p $(OBJDIR)
	$(CC) -MMD $(CFLAGS) -c $< -o $@

$(NAME): $(OBJS)
	$(LD) $(CFLAGS) -o $@ $^ $(LIBS)

# Display compiler information
info:
	@$(CC) -v 2>&1 | head -n 2

# Clean the build
clean:
	$(RM) $(OBJDIR)/*.o
	$(RM) $(OBJDIR)/*.d
	$(RM) $(NAME)

-include $(DEPS)
