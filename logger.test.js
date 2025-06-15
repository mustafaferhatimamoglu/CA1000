const fs = require('fs');
const assert = require('assert');
const { logMessage } = require('./logger');

const logFile = 'test.log';
if (fs.existsSync(logFile)) {
  fs.unlinkSync(logFile);
}

logMessage('test message', logFile).then(() => {
  setTimeout(() => {
    const data = fs.readFileSync(logFile, 'utf8');
    assert(data.includes('test message'));
    console.log('logger test passed');
  }, 100);
}).catch(err => {
  console.error('logger test failed');
  console.error(err);
  process.exit(1);
});
