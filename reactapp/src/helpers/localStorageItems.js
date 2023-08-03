export function setLocalStorageItemWithExpiration(key, value, expirationTimeInHours) {
    const now = new Date();
    const expirationDate = new Date(now.getTime() + expirationTimeInHours * 60**2 * 10**3);
    const item = { value, expirationDate: expirationDate.toISOString() };
    localStorage.setItem(key, JSON.stringify(item));
}
  
export function getLocalStorageItem(key) {
    const itemString = localStorage.getItem(key);
    if (!itemString) 
      return null;
  
    const item = JSON.parse(itemString);
    const expirationDate = new Date(item.expirationDate);
    if (expirationDate <= new Date()) {
      localStorage.removeItem(key);
      return null;
    }
  
    return item.value;
}
  
  