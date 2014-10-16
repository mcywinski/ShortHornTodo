var _loginToken = null;

function GetLoginToken() {
    if (_loginToken == null) { //Prevent from loading from the storage over and over again
        _loginToken = localStorage.getItem('LoginToken');
    }
    return _loginToken;
}

function SetLoginToken(token) {
    localStorage.setItem('LoginToken', token);
}

function DeleteLoginToken() {
    localStorage.removeItem('LoginToken');
    _loginToken = null;
}