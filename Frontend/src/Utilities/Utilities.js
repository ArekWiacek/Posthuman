// Retarded enum XD
class LogLevelType {
    static Trace = 0;
    static Info = 1;
    static Warning = 2;
    static Error = 3;
    static Special = 4;
    static Disabled = 0;

    static get TRACE() { return this.Trace; }
    static get INFO() { return this.Info; }
    static get WARNING() { return this.Warning; }
    static get ERROR() { return this.Error; }
    static get SPECIAL() { return this.SPECIAL }
    static get DISABLED() { return this.Disabled; }
}

const LoggingEnabled = true;
const LogLevel = LogLevelType.TRACE;

// Log SPECIAL - log level that beats all, it logs message no matter what log level is set 
export const LogS = (message) => {
    Log(message);
}

// Log ERROR - worst scenario
export const LogE = (message, error) => {
    if(LogLevel <= LogLevelType.ERROR) {
        console.error("[ERROR] " + message);
        buildExeptionMessage(error);
    }
}

// Log WARNING - important, emphasized info 
export const LogW = (message) => {
    if(LogLevel <= LogLevelType.WARNING)
        console.warn("[WARNING] " + message);
}

// Log INFORMATION 
export const LogI = (message) => {
    if(LogLevel <= LogLevelType.INFO)
        Log(message);
}

// Log TRACE - messages about program flow
export const LogT = (message) => {
    if(LogLevel <= LogLevelType.TRACE)
        Log(message);
}

export const Log = message => {
    if(LoggingEnabled) {
        console.log(message);
    }
}

export const FormatDate = (string) => {
    var options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(string).toLocaleDateString([],options);
}

const buildExeptionMessage = (error) => {
    var exMsg = "";
    if (error.message) {
        exMsg += error.message;
    }
    if (error.stack) {
        exMsg += ' | stack: ' + error.stack;
    }
}