image: gitpod/workspace-dotnet

# FROM gitpod/workspace-full:latest

# USER gitpod
# #.NET installed via .gitpod.yml task until the following issue is fixed: https://github.com/gitpod-io/gitpod/issues/5090
# ENV DOTNET_VERSION=6.0
# ENV DOTNET_ROOT=/workspace/.dotnet
# ENV PATH=$PATH:$DOTNET_ROOT


# FROM gitpod/workspace-full:latest

# USER gitpod

# # Install .NET Core 3.1 SDK binaries on Ubuntu 20.04
# # Source: https://dev.to/carlos487/installing-dotnet-core-in-ubuntu-20-04-6jh
# RUN mkdir -p /home/gitpod/dotnet && curl -fsSL https://download.visualstudio.microsoft.com/download/pr/f65a8eb0-4537-4e69-8ff3-1a80a80d9341/cc0ca9ff8b9634f3d9780ec5915c1c66/dotnet-sdk-3.1.201-linux-x64.tar.gz | tar xz -C /home/gitpod/dotnet
# ENV DOTNET_ROOT=/home/gitpod/dotnet
# ENV PATH=$PATH:/home/gitpod/dotnet