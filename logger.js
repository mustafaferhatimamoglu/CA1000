const winston = require('winston');
function createLogger(filename = 'app.log') {
  return winston.createLogger({
    transports: [
      new winston.transports.File({ filename })
    ],
    format: winston.format.simple(),
  });
}
function logMessage(message, filename = 'app.log') {
  const logger = createLogger(filename);
  logger.info(message);
  // Winston finishes writes asynchronously; wait for 'finish'
  return new Promise((resolve, reject) => {
    logger.on('finish', resolve);
    logger.end();
  });
}
if (require.main === module) {
  logMessage(process.argv[2]).catch(err => {
    console.error(err);
    process.exit(1);
  });
}
module.exports = { createLogger, logMessage };
