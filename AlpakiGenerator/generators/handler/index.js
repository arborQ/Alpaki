var Generator = require('yeoman-generator');

module.exports = class extends Generator {
    constructor(args, opts) {
        super(args, opts);
        this.option("name", { type: String, required: true });
        this.log(this.options.name);
    }
    // method1() {
    //     this.log('method 1 just ran');
    // }
    // async prompting() {
    //     const answers = await this.prompt([
    //         {
    //             type: "input",
    //             name: "controler",
    //             message: "Add as controller action?",
    //             default: `${this.options.name}Controller` // Default to current folder name
    //         },
    //         {
    //             type: "confirm",
    //             name: "unit",
    //             message: "Would you like to create unit tests?"
    //         }
    //     ]);

    //     // this.log("app name", answers.name);
    //     // this.log("cool feature", answers.cool);
    // }

    writing() {
        this.fs.copyTpl(
            this.templatePath('handler'),
            this.destinationPath(`Alpaki.Logic/Handlers/${this.options.name}/${this.options.name}Handler.cs`),
            { name: this.options.name }
        );

        this.fs.copyTpl(
            this.templatePath('request'),
            this.destinationPath(`Alpaki.Logic/Handlers/${this.options.name}/${this.options.name}Request.cs`),
            { name: this.options.name }
        );

        this.fs.copyTpl(
            this.templatePath('response'),
            this.destinationPath(`Alpaki.Logic/Handlers/${this.options.name}/${this.options.name}Response.cs`),
            { name: this.options.name }
        );

        this.fs.copyTpl(
            this.templatePath('validator'),
            this.destinationPath(`Alpaki.Logic/Handlers/${this.options.name}/${this.options.name}Validator.cs`),
            { name: this.options.name }
        );
    }
};