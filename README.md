# Logging test

##  Κώδικας

Θα χειασούμε το nuget AWS.Logger.AspNetCore

Στη Startup.cs στη ConfigureServices

    services.AddLogging(config =>
    {
        config.AddAWSProvider(Configuration.GetAWSLoggingConfigSection());
        config.SetMinimumLevel(LogLevel.Debug);
    });

Στο appsettings.json

    "Logging": {
	    "Region": "us-west-1",
	    "LogGroup": "Codehubtest2",
	    "IncludeLogLevel": true,
	    "IncludeCategory": true,
	    "IncludeNewline": true,
	    "IncludeException": true,
	    "IncludeEventId": false,
	    "IncludeScopes": false,
	    "LogLevel": {
	      "Default": "Debug",
	      "System": "Information",
	      "Microsoft": "Information"
	    }
    },

Πρέπει να προσέξουμε ότι το Region είναι το ίδιο με το region που θα κάνουμε publish.
To LogGroup θα δημιουργηθεί αυτόματα αν έχουμε τα κατάλληλα permissions.

Από εκεί και πέρα κάνουμε inject τον ILogger και τον χρησιμοποιούμε.

##  AWS
Θα πρέπει να δημιουργήσουμε ένα χρήστη(ΙΑΜ) με τα εξής permissions

 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [AmazonEC2FullAccess](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2FAmazonEC2FullAccess)


 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [IAMFullAccess](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2FIAMFullAccess)


 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [AmazonAPIGatewayPushToCloudWatchLogs](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2Fservice-role%2FAmazonAPIGatewayPushToCloudWatchLogs)


 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [CloudWatchFullAccess](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2FCloudWatchFullAccess)


 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [AWSElasticBeanstalkFullAccess](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2FAWSElasticBeanstalkFullAccess)


 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [CloudWatchLogsFullAccess](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2FCloudWatchLogsFullAccess)


 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [EC2InstanceProfileForImageBuilder](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2FEC2InstanceProfileForImageBuilder)


 ![](https://console.aws.amazon.com/iam/assets/images/policy_icon.png) [EC2InstanceConnect](https://console.aws.amazon.com/iam/home?region=us-west-1#/policies/arn%3Aaws%3Aiam%3A%3Aaws%3Apolicy%2FEC2InstanceConnect)

Με την δημιουργεία του χρήστη θα κατεβάσουμε από το aws και ένα csv αρχείο με credentials για να συνδεθούμε στο aws toolkit.

##  Visual studio
Θα χρειαστούμε το aws Toolkit extension for visual studio.
Σε αυτό θα συνδέσουμε ένα χρήστη που θα φτιάξουμε στο aws και με αυτό θα κάνουμε pusblish το app στο aws. Θα πρέπει αυτός ο χρήστης να έχει τα κατάλληλα permissions για να γίνει σωστά το pusblish και να δουλέψει το logging.

https://imgur.com/IcTZ2gp
![Προσθήκη καινούριου προφίλ](https://imgur.com/IcTZ2gp)
Κάνουμε import τα credentials του χρήστη που δημιουργήσαμε

https://imgur.com/j7qZdiI
![Publish to aws](https://imgur.com/j7qZdiI) 
Εφόσον έχουμε συνδεθεί στο aws toolkit μπορούμε να κάνουμε publish.

https://imgur.com/aB13s2E
![.](https://imgur.com/aB13s2E)
Για account profile αφήνουμε το default.
Region, πρέπει να είναι η ίδια με τη region που βάλαμε στο appsettings.json
Και δημιουργούμε καινούριο περιβάλλον.

https://imgur.com/65xXx40
![Επιλογή Environment](https://imgur.com/65xXx40)
Επιλογή url του app.

https://imgur.com/K7pOBd5
![EC2 config](https://imgur.com/K7pOBd5)
Επιλογή container και instance type. Βάζω πάντα Linux και t2.micro γιατί ξέρω είναι στο aws free.

https://imgur.com/LXgmv0z
![Επιλογή ρόλου](https://imgur.com/LXgmv0z)
Εδώ πρέπει να διαλέξουμε το cloudwatch full access από το dropdown.
Επίσης ο χρήστης μας πρέπει να έχει αυτό το permission.

https://imgur.com/GDNjp1l
![Build Configuration](https://imgur.com/GDNjp1l)
Επιλέγουμε Release και τέλος Finish.

#
Το app σε λίγο θα πρέπει να εμφανιστεί στο Elastic beanstalk μενού του aws.
Στο μενού cloudwatch -> log groups θα πρέπει να δημιουργηθεί ένα log group με το όνομα από το appsettings.json.
Μέσα θα πρέπει να έχει ένα Log stream για καθε ένα publish που γίνεται.
