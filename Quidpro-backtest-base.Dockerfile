FROM quantconnect/lean:foundation

# RUN git clone -b dev/cloud-backtest https://github.com/QuidPro/Lean.git

# RUN dotnet build /Lean/Launcher

COPY Launcher/bin/Debug/ /Lean/Launcher/bin/Debug/
COPY Optimizer.Launcher/bin/Debug/ /Lean/Optimizer.Launcher/bin/Debug/
